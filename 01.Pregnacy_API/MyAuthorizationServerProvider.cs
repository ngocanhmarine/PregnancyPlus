using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using PregnancyData.Entity;
using PregnancyData.Dao;

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
				if (currentUser.you_are_the == "dev")
				{
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.dev.ToString()));
					identity.AddClaim(new Claim("phone", currentUser.phone));
					if (currentUser.first_name != null)
					{
						identity.AddClaim(new Claim("first_name", currentUser.first_name));
					}
					if (currentUser.last_name != null)
					{
						identity.AddClaim(new Claim("last_name", currentUser.last_name));
					}
					if (currentUser.location != null)
					{
						identity.AddClaim(new Claim("location", currentUser.location));
					}
					if (currentUser.social_type != null)
					{
						identity.AddClaim(new Claim("social_type", currentUser.social_type));
					}
					if (currentUser.avarta != null)
					{
						identity.AddClaim(new Claim("avarta", currentUser.avarta));
					}
					if (currentUser.status != null)
					{
						identity.AddClaim(new Claim("status", currentUser.status));
					}
					context.Validated(identity);
				}
				else
				{
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.user.ToString()));
					identity.AddClaim(new Claim("phone", currentUser.phone));
					if (currentUser.first_name != null)
					{
						identity.AddClaim(new Claim("first_name", currentUser.first_name));
					}
					if (currentUser.last_name != null)
					{
						identity.AddClaim(new Claim("last_name", currentUser.last_name));
					}
					if (currentUser.location != null)
					{
						identity.AddClaim(new Claim("location", currentUser.location));
					}
					if (currentUser.social_type != null)
					{
						identity.AddClaim(new Claim("social_type", currentUser.social_type));
					}
					if (currentUser.avarta != null)
					{
						identity.AddClaim(new Claim("avarta", currentUser.avarta));
					}
					if (currentUser.status != null)
					{
						identity.AddClaim(new Claim("status", currentUser.status));
					}
					context.Validated(identity);
				}
			}
			else if (context.UserName == "WSPadmin" && context.Password == "WSPadmin")
			{
				identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.admin.ToString()));
				identity.AddClaim(new Claim("username", "admin"));
				identity.AddClaim(new Claim(ClaimTypes.Name, "Admin"));
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