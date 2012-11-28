using System.Linq;

namespace Proofted.Web.Models
{
    public class AdminSetup
    {
        private const string _administrators = "Administrators";
        private readonly string _userName;

        private AdminSetup(string userName)
        {
            _userName = userName;
        }

        public static void SetupAdminIfNecessary(string userName)
        {
            var setup = new AdminSetup(userName);
            setup.SetupAdminIfNecessary();
        }

        private void SetupAdminIfNecessary()
        {
            UserDbContext.Borrow(
                context =>
                {
                    var admin = GetAdministratorsRole(context);

                    if (admin == null)
                    {
                        CreateAdminAndAssociateUser(context);
                    }
                });
        }

        private void CreateAdminAndAssociateUser(UserDbContext context)
        {
            var admin = new webpages_Roles {RoleName = _administrators};
            context.webpages_Roles.Add(admin);

            var user = GetUserByName(context);
            if (user != null)
            {
                user.webpages_Roles.Add(admin);
            }
            context.SaveChanges();
        }

        private UserProfile GetUserByName(UserDbContext context)
        {
            var user = context.UserProfiles.SingleOrDefault(p => p.UserName == _userName);
            return user;
        }

        private static webpages_Roles GetAdministratorsRole(UserDbContext context)
        {
            var admin =
                context.webpages_Roles.SingleOrDefault(p => p.RoleName == _administrators);
            return admin;
        }
    }
}