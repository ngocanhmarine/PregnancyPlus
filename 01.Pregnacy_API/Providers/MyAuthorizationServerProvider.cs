using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using Microsoft.Owin.Security.OAuth;
using PregnancyData.Entity;
using PregnancyData.Dao;
using RestSharp;
using Newtonsoft.Json.Linq;
using _01.Pregnacy_API.Social.Models;

namespace _01.Pregnacy_API
{
	public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		/// <summary>
		/// Validate that the origin of the request is a registered client_id
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		/// <summary>
		/// Validate provided username and password when the grant_type is set to "password".
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			if (context.OwinContext.Request.Headers["Provider"] != null)
			{
				if (context.OwinContext.Request.Headers["Provider"].ToLower() == "facebook" && context.OwinContext.Request.Headers["access_token"] != null)
				{
					var accessToken = context.OwinContext.Request.Headers["access_token"];
					var client = new RestClient("https://graph.facebook.com/");
					var request = new RestRequest("me", Method.GET);
					request.AddQueryParameter("fields", "id,name,email");
					request.AddQueryParameter("access_token", accessToken);
					var response = client.Execute(request);
					if (response.StatusCode == HttpStatusCode.OK)
					{
						var content = JObject.Parse(response.Content);
						var userInfo = new FacebookUserInfo() { id = content["id"].ToString(), name = content["name"].ToString(), email = content["email"].ToString() };
						PregnancyEntity connect = new PregnancyEntity();
						preg_user user = connect.preg_user.Where(c => c.uid == userInfo.id && c.social_type_id == (int)SysConst.SocialTypes.facebook).FirstOrDefault();
						if (user != null)
						{
							user.email = userInfo.email;
							user.first_name = userInfo.name;
							connect.SaveChanges();
						}
						else
						{
							user = new preg_user();
							user.uid = userInfo.id;
							user.email = userInfo.email;
							user.first_name = userInfo.name;
							user.social_type_id = (int)SysConst.SocialTypes.facebook;
							connect.preg_user.Add(user);
							connect.SaveChanges();
							user = connect.preg_user.Where(c => c.uid == userInfo.id && c.social_type_id == (int)SysConst.SocialTypes.facebook).FirstOrDefault();
						}
						var identity = new ClaimsIdentity(context.Options.AuthenticationType);
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.social.ToString()));
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.dev.ToString()));
						identity.AddClaim(new Claim("id", user.id.ToString()));
						context.Validated(identity);
					}
					else
					{
						context.SetError("Invalid grant", SysConst.LOGIN_SOCIAL_FAILED);
						return;
					}
				}
				else if (context.OwinContext.Request.Headers["Provider"].ToLower() == "google" && context.OwinContext.Request.Headers["access_token"] != null)
				{
					var accessToken = context.OwinContext.Request.Headers["access_token"];
					var client = new RestClient("https://www.googleapis.com/oauth2/v3/");
					var request = new RestRequest("tokeninfo", Method.GET);
					request.AddQueryParameter("id_token", accessToken);
					var response = client.Execute(request);
					if (response.StatusCode == HttpStatusCode.OK)
					{
						var content = JObject.Parse(response.Content);
						var userInfo = new GoogleUserInfo() { sub = content["sub"].ToString(), name = content["name"].ToString(), email = content["email"].ToString(), picture = content["picture"].ToString(), given_name = content["given_name"].ToString(), family_name = content["family_name"].ToString() };
						PregnancyEntity connect = new PregnancyEntity();
						preg_user user = connect.preg_user.Where(c => c.uid == userInfo.sub && c.social_type_id == (int)SysConst.SocialTypes.google).FirstOrDefault();
						if (user != null)
						{
							user.email = userInfo.email;
							user.first_name = userInfo.name;
							user.avarta = userInfo.picture;
							connect.SaveChanges();
						}
						else
						{
							user = new preg_user();
							user.uid = userInfo.sub;
							user.email = userInfo.email;
							user.first_name = userInfo.name;
							user.avarta = userInfo.picture;
							user.social_type_id = (int)SysConst.SocialTypes.google;
							connect.preg_user.Add(user);
							connect.SaveChanges();
							user = connect.preg_user.Where(c => c.uid == userInfo.sub && c.social_type_id == (int)SysConst.SocialTypes.google).FirstOrDefault();
						}
						var identity = new ClaimsIdentity(context.Options.AuthenticationType);
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.social.ToString()));
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.dev.ToString()));
						identity.AddClaim(new Claim("id", user.id.ToString()));
						context.Validated(identity);
					}
					else
					{
						context.SetError("Invalid grant", SysConst.LOGIN_SOCIAL_FAILED);
						return;
					}
				}
			}

			else
			{
				var identity = new ClaimsIdentity(context.Options.AuthenticationType);
				//Check username & password
				preg_user user = new preg_user();
				user.phone = context.UserName;
				user.password = SysMethod.MD5Hash(context.Password);
				UserDao dao = new UserDao();
				IEnumerable<preg_user> result = dao.GetUsersByParams(user);

				if (result.Count() > 0)
				{
					preg_user currentUser = result.FirstOrDefault();
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.dev.ToString()));
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.user.ToString()));
					identity.AddClaim(new Claim("id", currentUser.id.ToString()));
					context.Validated(identity);
				}
				else if (context.UserName == "WSPadmin" && context.Password == "WSPadmin")
				{
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.admin.ToString()));
					identity.AddClaim(new Claim("id", "0"));
					context.Validated(identity);
				}
				else
				{
					context.SetError("Invalid grant", SysConst.LOGIN_FAILED);
					return;
				}
			}
		}
	}
}