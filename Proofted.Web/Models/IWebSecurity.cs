namespace Proofted.Web.Models
{
    using System.Security.Principal;

    public interface IWebSecurity
    {
        #region Public Properties

        IPrincipal CurrentUser { get; }

        #endregion

        #region Public Methods and Operators

        bool ChangePassword(string userName, string currentPassword, string newPassword);

        string CreateAccount(string userName, string password, bool requireConfirmationToken = false);

        string CreateUserAndAccount(
            string userName, string password, object propertyValues = null, bool requireConfirmationToken = false);

        int GetUserId(string userName);

        bool Login(string userName, string password, bool persistCookie = false);

        void Logout();

        #endregion

        void InitializeDatabaseConnection(string defaultconnection, string userprofile, string userid, string username, bool autoCreateTables);
    }
}