﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.MicrosoftAccount;
using NuGetGallery.Configuration;
using Owin;

namespace NuGetGallery.Authentication.Providers.MicrosoftAccount
{
    public class MicrosoftAccountAuthenticator : Authenticator<MicrosoftAccountAuthenticatorConfiguration>
    {
        public static readonly string DefaultAuthenticationType = "MicrosoftAccount";

        protected override void AttachToOwinApp(ConfigurationService config, IAppBuilder app)
        {
            var options = new MicrosoftAccountAuthenticationOptions();
            options.Scope.Add("wl.emails");
            Config.ApplyToOwinSecurityOptions(options);
            app.UseMicrosoftAccountAuthentication(options);
        }

        public override AuthenticatorUI GetUI()
        {
            return new AuthenticatorUI()
            {
                SignInMessage = Strings.MicrosoftAccount_SignInMessage
            };
        }

        public override ActionResult Challenge(string redirectUrl)
        {
            return new ChallengeResult(BaseConfig.AuthenticationType, redirectUrl);
        }
    }
}