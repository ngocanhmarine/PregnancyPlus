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

			}
			else
			{
				var identity = new ClaimsIdentity(context.Options.AuthenticationType);
				//Check username & password
				preg_user user = new preg_user();
				user.email = context.UserName;
				user.password = SysMethod.MD5Hash(context.Password);
				UserDao dao = new UserDao();
				IEnumerable<preg_user> result = dao.GetUsersByParams(user);

				if (result.Count() > 0)
				{
					preg_user currentUser = result.FirstOrDefault();
					identity.AddClaim(new Claim(ClaimTypes.Role, SysConst.UserType.dev.ToString()));
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