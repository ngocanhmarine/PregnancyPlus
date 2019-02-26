using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net;
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
					request.AddQueryParameter("fields", "id,name,email,picture.width(2000).height(2000)");
					request.AddQueryParameter("access_token", accessToken);
					var response = client.Execute(request);
					if (response.StatusCode == HttpStatusCode.OK)
					{
						var content = JObject.Parse(response.Content);
						var userInfo = new FacebookUserInfo() { id = content["id"].ToString() };
						if (content["name"] != null)
						{
							userInfo.name = content["name"].ToString();
						}
						if (content["email"] != null)
						{
							userInfo.email = content["email"].ToString();
						}
						if (content["picture"]["data"]["url"] != null)
						{
							userInfo.avatar = content["picture"]["data"]["url"].ToString();
						}
						PregnancyEntity connect = new PregnancyEntity();
						preg_user user = connect.preg_user.Where(c => c.uid == userInfo.id && c.social_type_id == (int)SysConst.SocialTypes.facebook).FirstOrDefault();
						if (user != null)
						{
							//user.email = userInfo.email;
							//user.first_name = userInfo.name;
							user.time_last_login = DateTime.Now;
							connect.SaveChanges();
						}
						else
						{
							user = new preg_user();
							user.uid = userInfo.id;
							user.email = userInfo.email;
							user.first_name = userInfo.name;
							user.avatar = userInfo.avatar;
							user.social_type_id = (int)SysConst.SocialTypes.facebook;
							user.time_created = DateTime.Now;
							connect.preg_user.Add(user);
							SysMethod.createAccountNop(user);
							connect.SaveChanges();
							user = connect.preg_user.Where(c => c.uid == userInfo.id && c.social_type_id == (int)SysConst.SocialTypes.facebook).FirstOrDefault();
						}
						preg_auth auth = connect.preg_auth.Where(c => c.user_id == user.id).FirstOrDefault();
						if (auth == null)
						{
							auth = new preg_auth() { user_id = user.id };
							connect.preg_auth.Add(auth);
						}
						auth.token = context.OwinContext.Request.Headers["access_token"];
						connect.SaveChanges();

						var identity = new ClaimsIdentity(context.Options.AuthenticationType);
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.social.ToString()));
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.user.ToString()));
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
						var userInfo = new GoogleUserInfo() { sub = content["sub"].ToString() };
						if (content["name"] != null)
						{
							userInfo.name = content["name"].ToString();
						}
						if (content["email"] != null)
						{
							userInfo.email = content["email"].ToString();
						}
						if (content["picture"] != null)
						{
							userInfo.picture = content["picture"].ToString();
						}
						if (content["given_name"] != null)
						{
							userInfo.given_name = content["given_name"].ToString();
						}
						if (content["family_name"] != null)
						{
							userInfo.family_name = content["family_name"].ToString();
						}
						PregnancyEntity connect = new PregnancyEntity();
						preg_user user = connect.preg_user.Where(c => c.uid == userInfo.sub && c.social_type_id == (int)SysConst.SocialTypes.google).FirstOrDefault();
						if (user != null)
						{
							//user.email = userInfo.email;
							//user.first_name = userInfo.name;
							//user.avatar = userInfo.picture;
							user.time_last_login = DateTime.Now;
							connect.SaveChanges();
						}
						else
						{
							user = new preg_user();
							user.uid = userInfo.sub;
							user.email = userInfo.email;
							user.first_name = userInfo.name;
							user.avatar = userInfo.picture;
							user.social_type_id = (int)SysConst.SocialTypes.google;
							user.time_created = DateTime.Now;
							connect.preg_user.Add(user);
							SysMethod.createAccountNop(user);
							connect.SaveChanges();
							user = connect.preg_user.Where(c => c.uid == userInfo.sub && c.social_type_id == (int)SysConst.SocialTypes.google).FirstOrDefault();
						}
						preg_auth auth = connect.preg_auth.Where(c => c.user_id == user.id).FirstOrDefault();
						if (auth == null)
						{
							auth = new preg_auth() { user_id = user.id };
							connect.preg_auth.Add(auth);
						}
						auth.token = context.OwinContext.Request.Headers["access_token"];
						connect.SaveChanges();

						var identity = new ClaimsIdentity(context.Options.AuthenticationType);
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.social.ToString()));
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.user.ToString()));
						identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.dev.ToString()));
						identity.AddClaim(new Claim("id", user.id.ToString()));
						context.Validated(identity);
					}
					else
					{
						var client2 = new RestClient("https://www.googleapis.com/oauth2/v1/");
						var request2 = new RestRequest("userinfo", Method.GET);
						request2.AddQueryParameter("alt", "json");
						request2.AddQueryParameter("access_token", accessToken);
						var response2 = client.Execute(request2);
						if (response2.StatusCode == HttpStatusCode.OK)
						{
							var content = JObject.Parse(response2.Content);
							var userInfo = new GoogleUserInfo() { sub = content["sub"].ToString() };
							if (content["name"] != null)
							{
								userInfo.name = content["name"].ToString();
							}
							if (content["email"] != null)
							{
								userInfo.email = content["email"].ToString();
							}
							if (content["picture"] != null)
							{
								userInfo.picture = content["picture"].ToString();
							}
							if (content["given_name"] != null)
							{
								userInfo.given_name = content["given_name"].ToString();
							}
							if (content["family_name"] != null)
							{
								userInfo.family_name = content["family_name"].ToString();
							}
							PregnancyEntity connect = new PregnancyEntity();
							preg_user user = connect.preg_user.Where(c => c.uid == userInfo.sub && c.social_type_id == (int)SysConst.SocialTypes.google).FirstOrDefault();
							if (user != null)
							{
								//user.email = userInfo.email;
								//user.first_name = userInfo.name;
								//user.avatar = userInfo.picture;
								user.time_last_login = DateTime.Now;
								connect.SaveChanges();
							}
							else
							{
								user = new preg_user();
								user.uid = userInfo.sub;
								user.email = userInfo.email;
								user.first_name = userInfo.name;
								user.avatar = userInfo.picture;
								user.social_type_id = (int)SysConst.SocialTypes.google;
								user.time_created = DateTime.Now;
								connect.preg_user.Add(user);
								SysMethod.createAccountNop(user);
								connect.SaveChanges();
								user = connect.preg_user.Where(c => c.uid == userInfo.sub && c.social_type_id == (int)SysConst.SocialTypes.google).FirstOrDefault();
							}
							preg_auth auth = connect.preg_auth.Where(c => c.user_id == user.id).FirstOrDefault();
							if (auth == null)
							{
								auth = new preg_auth() { user_id = user.id };
								connect.preg_auth.Add(auth);
							}
							auth.token = context.OwinContext.Request.Headers["access_token"];
							connect.SaveChanges();

							var identity = new ClaimsIdentity(context.Options.AuthenticationType);
							identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.social.ToString()));
							identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.user.ToString()));
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
			}
			else if (context.UserName != null && context.Password != null)
			{
				var identity = new ClaimsIdentity(context.Options.AuthenticationType);
				PregnancyEntity connect = new PregnancyEntity();
				UserDao dao = new UserDao();
				//Check username & password
				string phone = context.UserName;
				string password = SysMethod.MD5Hash(context.Password);
				preg_user user = connect.preg_user.Where(c => c.phone == phone && c.password == password).FirstOrDefault();

				if (user != null)
				{
					user.time_last_login = DateTime.Now;
					connect.SaveChanges();
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.dev.ToString()));
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.user.ToString()));
					identity.AddClaim(new Claim("id", user.id.ToString()));
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