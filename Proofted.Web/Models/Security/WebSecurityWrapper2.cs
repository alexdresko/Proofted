namespace Proofted.Web.Models.Security
{
    using System.Security.Principal;
    using System.Web;

    public class WebSecurityWrapper2 : IWebSecurity
    {
        private readonly IWebSecurity _webSecurity;

        public WebSecurityWrapper2(IWebSecurity webSecurity)
        {
            this._webSecurity = webSecurity;
        }

        public bool Login(string userName, string password, bool persistCookie = false)
        {
            return this._webSecurity.Login(userName, password, persistCookie);
        }

        public void Logout()
        {
            this._webSecurity.Logout();
        }

        public string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        {
            var account = this._webSecurity.CreateUserAndAccount(userName, password, propertyValues, requireConfirmationToken);
            AdminSetup.SetupAdminIfNecessary(userName);
            return account;
        }

        public int GetUserId(string userName)
        {
            return this._webSecurity.GetUserId(userName);
        }

        public bool ChangePassword(string userName, string currentPassword, string newPassword)
        {
            return this._webSecurity.ChangePassword(userName, currentPassword, newPassword);
        }

        public string CreateAccount(string userName, string password, bool requireConfirmationToken = false)
        {
            var account = this._webSecurity.CreateAccount(userName, password, requireConfirmationToken);
            return account;
        }

        public IPrincipal CurrentUser
        {
            get { return HttpContext.Current.User; }
        }

        public void InitializeDatabaseConnection(string defaultconnection, string userprofile, string userid, string username, bool autoCreateTables)
        {
            this._webSecurity.InitializeDatabaseConnection(defaultconnection, userprofile, userid, username, autoCreateTables);
        }
    }
}