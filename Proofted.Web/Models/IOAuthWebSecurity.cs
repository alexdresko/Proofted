using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proofted.Web.Models
{
    using DotNetOpenAuth.AspNet;

    using Microsoft.Web.WebPages.OAuth;

    public interface IOAuthWebSecurity
    {
        string GetUserName(string providerName, string providerUserId);

        bool HasLocalAccount(int userId);

        ICollection<OAuthAccount> GetAccountsFromUserName(string userName);

        bool DeleteAccount(string providerName, string providerUserId);

        AuthenticationResult VerifyAuthentication(string returnUrl);

        bool Login(string providerName, string providerUserId, bool createPersistentCookie);

        void CreateOrUpdateAccount(string providerName, string providerUserId, string userName);

        string SerializeProviderUserId(string providerName, string providerUserId);

        AuthenticationClientData GetOAuthClientData(string providerName);

        bool TryDeserializeProviderUserId(string data, out string providerName, out string providerUserId);

        ICollection<AuthenticationClientData> RegisteredClientData { get; }

        void RequestAuthentication(string provider, string returnUrl);

        void RegisterFacebookClient(string appId, string secretKey);

        void RegisterGoogleClient();
    }
}
