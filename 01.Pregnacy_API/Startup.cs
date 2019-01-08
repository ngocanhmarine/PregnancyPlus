﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using _01.Pregnacy_API.Models;

[assembly: OwinStartup(typeof(_01.Pregnacy_API.Startup))]

namespace _01.Pregnacy_API
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			// Configure the db context, user manager and signin manager to use a single instance per request
			app.CreatePerOwinContext(ApplicationDbContext.Create);
			app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
			app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

			#region Cookies
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/account/login"),
				Provider = new CookieAuthenticationProvider
				{
					// Enables the application to validate the security stamp when the user logs in. This is a security feature which is used when you change a password or add an external login to your account.
					OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
						validateInterval: SysConst.AccessTokenExpiredTimeSpan,
						regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager)
						)
				}
			});
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
			#endregion

			#region Use Bearer Authorization

			// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
			app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

			var myProvider = new MyAuthorizationServerProvider();
			OAuthAuthorizationServerOptions options = new OAuthAuthorizationServerOptions
			{
				AllowInsecureHttp = true,
				TokenEndpointPath = new PathString("/api/token"),
				AuthorizeEndpointPath = new PathString("/api/account/externallogin"),
				AccessTokenExpireTimeSpan = SysConst.AccessTokenExpiredTimeSpan,
				Provider = myProvider
			};
			app.UseOAuthBearerTokens(options);
			#endregion

			#region Use google authentication
			app.UseGoogleAuthentication(
				clientId: "847363778687-nfritbg3svps0pm791o5tbdfpkmijo7r.apps.googleusercontent.com",
				clientSecret: "FAfihg1Tw0xeWV4nY9R0Ld4n"
				);
			#endregion

			#region Use facebook authentication
			app.UseFacebookAuthentication(
				appId: "2255664454668775",
				appSecret: "994ab93f9bfd5d4c3f519d479d1fe7fe"
			);
			#endregion

			#region Use twitter authentication
			app.UseTwitterAuthentication(
				consumerKey: "Twitter API Key",
				consumerSecret: "Twitter API Secret"
			);
			#endregion

			HttpConfiguration config = new HttpConfiguration();
			WebApiConfig.Register(config);
			//ConfigureAuth(app);
		}
	}
}
