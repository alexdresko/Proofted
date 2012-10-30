// ReSharper disable InconsistentNaming
namespace Proofted.Web.Tests.OAuthSettings
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    //using Microsoft.Web.WebPages.OAuth;
    using Microsoft.Web.WebPages.OAuth;

    using Moq;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    using Proofted.Web.Core;
    using Proofted.Web.Migrations;
    using Proofted.Web.Models;
    using Proofted.Web.Tests.Utilities;

    using Configuration = Proofted.Web.Migrations.Configuration;

    [TestClass]
    public class OAuthRegistrarTests
    {
        #region Public Methods and Operators

        [TestMethod]
        public void RegisterFacebookClient_GivenZeroRecords_DoesNotRegister()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(this.TestContext);

            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            var mock = fixture.Freeze<Mock<IOAuthWebSecurity>>();
            var reg = fixture.CreateAnonymous<OAuthRegistrar>();

            reg.RegisterFacebookClient();

            mock.Verify(p => p.RegisterFacebookClient(It.IsAny<string>(), It.IsAny<string>()), Times.Never());

            Assert.AreEqual(0, mock.Object.RegisteredClientData.Count);
        }

        [TestMethod]
        public void RegisterFacebookClient_GivenOneRecord_Registers()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(this.TestContext);

            var fixture = new Fixture().Customize(new AutoMoqCustomization());


            var faceBookAppCredential = fixture.Build<FaceBookAppCredential>().With(p => p.Active, true).CreateAnonymous();

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

        public TestContext TestContext { get; set; }

        #endregion

        #region Methods


        #endregion


    }
}