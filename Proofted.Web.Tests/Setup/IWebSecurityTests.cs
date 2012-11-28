using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Proofted.Web.Models;
using Proofted.Web.Tests.OAuthSettings;
using Proofted.Web.Tests.Utilities;

namespace Proofted.Web.Tests.Setup
{
    [TestClass]
    public class WebSecurityTests
    {
        private IFixture _fixture;
        private WebSecurityWrapper2 _security;
        private Mock<IWebSecurity> _securityMock;
        public TestContext TestContext { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(TestContext);
            _fixture = FixtureFactory.CreateFixture();
            _securityMock = _fixture.CreateAnonymous<Mock<IWebSecurity>>();
            _security = new WebSecurityWrapper2(_securityMock.Object);
        }

        [TestMethod]
        public void CreateUserAndAccount_GivenNoExistingRolesAndNoUsers_SetsFirstAdministrator()
        {
            VerifyRoleCount(0);

            RegisterUser();
            VerifyRoleCount(1);
        }

        private static void VerifyRoleCount(int expected)
        {
            UserDbContext.Borrow(context => Assert.AreEqual(expected, context.webpages_Roles.Count()));
        }

        private void RegisterUser()
        {
            var username = _fixture.CreateAnonymous<string>();
            var password = _fixture.CreateAnonymous<string>();
            _security.CreateUserAndAccount(username, password);
        }

        [TestMethod]
        public void CreateUserAndAccount_GivenExistingAdministrator_DoesNothing()
        {
            UserDbContext.Borrow(
                context =>
                {
                    var user = CreateUser(context);
                    var admin = CreateAdminRole(context);
                    AssociateUserWithAdminRole(user, admin);
                    context.SaveChanges();
                });

            VerifyRoleCount(1);

            RegisterUser();

            VerifyRoleCount(1);
        }

        private static void AssociateUserWithAdminRole(UserProfile user, webpages_Roles admin)
        {
            user.webpages_Roles.Add(admin);
        }

        private static webpages_Roles CreateAdminRole(UserDbContext context)
        {
            var admin = new webpages_Roles {RoleName = "Administrators"};
            context.webpages_Roles.Add(admin);
            return admin;
        }

        private UserProfile CreateUser(UserDbContext context)
        {
            var user = _fixture.CreateAnonymous<UserProfile>();
            context.UserProfiles.Add(user);
            return user;
        }
    }
}