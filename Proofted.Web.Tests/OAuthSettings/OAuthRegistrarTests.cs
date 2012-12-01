// ReSharper disable InconsistentNaming

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using Proofted.Web.Core;
using Proofted.Web.Models;
using Proofted.Web.Tests.Utilities;

namespace Proofted.Web.Tests.OAuthSettings
{
    //using Microsoft.Web.WebPages.OAuth;

    [TestClass]
    public class OAuthRegistrarTests
    {
        #region Public Methods and Operators

        public TestContext TestContext { get; set; }

        [TestMethod]
        public void RegisterFacebookClient_GivenZeroRecords_DoesNotRegister()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(TestContext);

            var fixture = FixtureFactory.CreateFixture();

            var mock = fixture.Freeze<Mock<IOAuthWebSecurity>>();
            var reg = fixture.CreateAnonymous<OAuthRegistrar>();

            reg.RegisterFacebookClient();

            mock.Verify(p => p.RegisterFacebookClient(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

            Assert.AreEqual(0, mock.Object.RegisteredClientData.Count);
        }

        [TestMethod]
        public void RegisterFacebookClient_GivenOneRecord_Registers()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(TestContext);

            var fixture = FixtureFactory.CreateFixture();


            var faceBookAppCredential =
                fixture.Build<FaceBookAppCredential>().With(p => p.Active, true).CreateAnonymous();

            ProoftedDbContext.Borrow(context =>
                                     {
                                         context.FaceBookAppCredentials.Add(
                                             faceBookAppCredential);

                                         context.SaveChanges();
                                     });

            var mock = fixture.Freeze<Mock<IOAuthWebSecurity>>();

            var reg = fixture.CreateAnonymous<OAuthRegistrar>();
            reg.RegisterFacebookClient();

            mock.Verify(p => p.RegisterFacebookClient(faceBookAppCredential.AppId, faceBookAppCredential.SecretKey));
        }

        #endregion

        #region Methods

        #endregion
    }
}