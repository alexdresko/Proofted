using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Proofted.Web.Tests.Setup
{
    using System.Linq;

    using Ploeh.AutoFixture;

    using Proofted.Web.Models;
    using Proofted.Web.Tests.Utilities;

    [TestClass]
    public class AdministratorSetupTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        public void TestFirstAdministrator_GivenNoUsers_SetsFirstAdministrator()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(TestContext);

            ProoftedDbContext.Borrow(context => Assert.AreEqual(0, context.webpages_Roles.Count()));
            
            var security = new WebSecurityWrapper();

            var fixture = new Fixture();



            var username = fixture.CreateAnonymous<string>();
            var password = fixture.CreateAnonymous<string>();
            security.CreateUserAndAccount(username, password);
        }
    }
}
