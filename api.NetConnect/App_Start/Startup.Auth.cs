using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace api.NetConnect
{
	public partial class Startup
	{
		public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType("Application");

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "Application",
                AuthenticationMode = AuthenticationMode.Active,
                LoginPath = new PathString("/login"),
				LogoutPath = new PathString("/logout"),
				CookieName = Properties.Settings.Default.AuthCookieName,
				CookieDomain = Properties.Settings.Default.AuthCookieName,
				SlidingExpiration = true,
				ExpireTimeSpan = TimeSpan.FromMinutes(Properties.Settings.Default.AuthCookieExpireTimeSpanMinutes),

				Provider = new CookieAuthenticationProvider
                {
					OnApplyRedirect = context =>
                    {
                        Uri returnUrl;
                        if (context.Request.Headers.Any(kv => kv.Key == "Referer"))
                            returnUrl = new Uri(context.Request.Headers.Single(kv => kv.Key == "Referer").Value[0]);
                        else
                            returnUrl = context.Request.Uri;

						if(context.Response.StatusCode == 401 && !isUriPartOfLogin(returnUrl))
                        {
                            String escapedReturlUrl = Uri.EscapeDataString(returnUrl.ToString());
                            context.Response.Headers.Add("X-Redirect", new String[] { this.AuthLoginUri(escapedReturlUrl).ToString() });
                        }
                    }
                }
            });
        }

		protected Uri AuthLoginUri(String returnUrl = null)
        {
            UriBuilder uriBuilder = new UriBuilder(Properties.Settings.Default.LoginAbosulteUrl);

            if (!String.IsNullOrWhiteSpace(returnUrl))
                uriBuilder.Query = String.Format("returnUrl={0}", returnUrl);

            return uriBuilder.Uri;
        }

		protected Boolean isUriPartOfLogin(Uri uri)
        {
            String requestUri = uri.AbsoluteUriWithoutQuery();
			String loginUri = AuthLoginUri().AbsoluteUriWithoutQuery();

            requestUri = requestUri.TrimEnd(new char[] { '/', '\\' });
			loginUri = loginUri.TrimEnd(new char[] { '/', '\\' });

            return String.Equals(requestUri, loginUri, StringComparison.InvariantCultureIgnoreCase);
        }
	}

	public static class UriExtension
    {
		public static String AbsoluteUriWithoutQuery(this Uri uri)
        {
            if (String.IsNullOrWhiteSpace(uri.Query))
                return uri.AbsoluteUri;
            else
                return uri.AbsoluteUri.Replace(uri.Query, "");
        }
    }
}