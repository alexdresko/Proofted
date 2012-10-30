namespace Proofted.Web.Core
{
    using System;
    using System.Collections.Generic;

    using Proofted.Web.Models;

    public class OAuthRegistrar
    {
        #region Fields

        private readonly IOAuthWebSecurity oAuthWebSecurityWrapper;

        #endregion

        #region Constructors and Destructors

        public OAuthRegistrar(IOAuthWebSecurity oAuthWebSecurityWrapper)
        {
            this.oAuthWebSecurityWrapper = oAuthWebSecurityWrapper;
        }

        #endregion

        #region Public Methods and Operators

        public void RegisterFacebookClient()
        {
            var settings = OAuthSettingsRepository.GetFaceBookSettings();
            ExecuteIfValid(
                settings,
                "Facebook",
                setting => (this.oAuthWebSecurityWrapper).RegisterFacebookClient(setting.AppId, setting.SecretKey));
        }

        #endregion

        #region Methods

        protected static void ExecuteIfValid<T>(List<T> things, string settingType, Action<T> action)
        {
            if (things != null)
            {
                if (things.Count == 1)
                {
                    action(things[0]);
                }

                if (things.Count > 1)
                {
                    throw new TooManyOAuthSettingsException(settingType);
                }
            }
        }

        #endregion

        public void RegisterGoogleClient()
        {
            this.oAuthWebSecurityWrapper.RegisterGoogleClient();
        }
    }
}