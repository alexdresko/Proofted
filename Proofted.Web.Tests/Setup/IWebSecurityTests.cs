namespace Proofted.Web.Tests.Setup
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Ploeh.AutoFixture;

    using Proofted.Web.Core;
    using Proofted.Web.Core.Exceptions;
    using Proofted.Web.Models;
    using Proofted.Web.Models.Security;
    using Proofted.Web.Tests.OAuthSettings;
    using Proofted.Web.Tests.Utilities;

    [TestClass]
    public class WebSecurityTests
    {
        #region Fields

        private IFixture _fixture;

        private WebSecurityWrapper2 _security;

        private Mock<IWebSecurity> _securityMock;

        #endregion

        #region Public Properties

        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void CreateUserAndAccount_GivenExistingAdministrator_DoesNothing()
        {
            UserDbContext.Borrow(
                context =>
                    {
                        var user = this.CreateUser(context);
                        var admin = CreateAdminRole(context);
                        AssociateUserWithAdminRole(user, admin);
                        context.SaveChanges();
                    });

            VerifyRoleCount(1);

            this.RegisterUser();

            VerifyRoleCount(1);
        }

        [TestMethod]
        public void CreateUserAndAccount_GivenNoExistingRolesAndNoUsers_SetsFirstAdministrator()
        {
            VerifyRoleCount(0);

            this.RegisterUser();
            VerifyRoleCount(1);
        }

        [TestMethod]
        [ExpectedException(typeof(CanNotCreateAdministratorException))]
        public void CreateUserAndAccount_GivenNoExistingRolesAndExistingUsers_ThrowsException()
        {
            UserDbContext.Borrow(
                context =>
                    {
                        this.CreateUser(context);
                        this.CreateUser(context);
                        context.SaveChanges();
                    });


            try
            {
                this.RegisterUser();
            }
            catch (CanNotCreateAdministratorException e)
            {
                VerifyRoleCount(1);
                throw;
            }
        }


        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(this.TestContext);
            this._fixture = FixtureFactory.CreateFixture();
            this._securityMock = this._fixture.Create<Mock<IWebSecurity>>();
            this._security = new WebSecurityWrapper2(this._securityMock.Object);
        }

        #endregion

        #region Methods

        private static void AssociateUserWithAdminRole(UserProfile user, webpages_Roles admin)
        {
            user.webpages_Roles.Add(admin);
        }

        private static webpages_Roles CreateAdminRole(UserDbContext context)
        {
            var admin = new webpages_Roles { RoleName = "Administrators" };
            context.webpages_Roles.Add(admin);
            return admin;
        }

        private static void VerifyRoleCount(int expected)
        {
            UserDbContext.Borrow(context => Assert.AreEqual(expected, context.webpages_Roles.Count()));
        }

        private UserProfile CreateUser(UserDbContext context)
        {
            var user = this._fixture.Create<UserProfile>();
            context.UserProfiles.Add(user);
            return user;
        }

        private void RegisterUser()
        {
            var username = this._fixture.Create<string>();
            var password = this._fixture.Create<string>();
            this._security.CreateUserAndAccount(username, password);
        }

        #endregion
    }
}