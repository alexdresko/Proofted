namespace Proofted.Web.Models.Security
{
    using System.Collections.Generic;

    using DotNetOpenAuth.AspNet;

    using Microsoft.Web.WebPages.OAuth;

    public class OAuthWebSecurityWrapper2 : IOAuthWebSecurity
    {
        private readonly IOAuthWebSecurity _oAuthWebSecurity;

        public OAuthWebSecurityWrapper2(IOAuthWebSecurity oAuthWebSecurity)
        {
            this._oAuthWebSecurity = oAuthWebSecurity;
        }
        public string GetUserName(string providerName, string providerUserId)
        {
            return this._oAuthWebSecurity.GetUserName(providerName, providerUserId);
        }

        public bool HasLocalAccount(int userId)
        {
            return this._oAuthWebSecurity.HasLocalAccount(userId);
        }

        public ICollection<OAuthAccount> GetAccountsFromUserName(string userName)
        {
            return this._oAuthWebSecurity.GetAccountsFromUserName(userName);
        }

        public bool DeleteAccount(string providerName, string providerUserId)
        {
            return this._oAuthWebSecurity.DeleteAccount(providerName, providerUserId);
        }

        public AuthenticationResult VerifyAuthentication(string returnUrl)
        {
            return this._oAuthWebSecurity.VerifyAuthentication(returnUrl);
        }

        public bool Login(string providerName, string providerUserId, bool createPersistentCookie)
        {
            return this._oAuthWebSecurity.Login(providerName, providerUserId, createPersistentCookie);
        }

        public void CreateOrUpdateAccount(string providerName, string providerUserId, string userName)
        {
            this._oAuthWebSecurity.CreateOrUpdateAccount(providerName, providerUserId, userName);
        }

        public string SerializeProviderUserId(string providerName, string providerUserId)
        {
            return this._oAuthWebSecurity.SerializeProviderUserId(providerName, providerUserId);
        }

        public AuthenticationClientData GetOAuthClientData(string providerName)
        {
            return this._oAuthWebSecurity.GetOAuthClientData(providerName);
        }

        public bool TryDeserializeProviderUserId(string data, out string providerName, out string providerUserId)
        {
            return this._oAuthWebSecurity.TryDeserializeProviderUserId(data, out providerName, out providerUserId);
        }

        public ICollection<AuthenticationClientData> RegisteredClientData
        {
            get { return this._oAuthWebSecurity.RegisteredClientData; }
        }

        public void RequestAuthentication(string provider, string returnUrl)
        {
            this._oAuthWebSecurity.RequestAuthentication(provider, returnUrl);
        }

        public void RegisterFacebookClient(string appId, string secretKey)
        {
            this._oAuthWebSecurity.RegisterFacebookClient(appId, secretKey);
        }

        public void RegisterGoogleClient()
        {
            this._oAuthWebSecurity.RegisterGoogleClient();
        }
    }
}