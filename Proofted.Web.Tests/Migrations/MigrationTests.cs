namespace Proofted.Web.Tests.Migrations
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Proofted.Web.Tests.Utilities;

    [TestClass]
    public class MigrationTests
    {
        #region Public Properties

        public TestContext TestContext { get; set; }

        #endregion

        #region Public Methods and Operators

        [TestMethod]
        public void CanUpgradeDownGradeAndUpgradeTheDatabase()
        {
            DatabaseMigrations.DeleteAndUpgradeDatabase(TestContext);
            DatabaseMigrations.UpdateDatabase(TestContext, targetMigration: "0");
            DatabaseMigrations.UpdateDatabase(TestContext);
            //var migrator = DatabaseMigrations.GetMigrator(this.TestContext);
            
            //migrator.Update();

            //migrator.Update(targetMigration: "0"); //Down
            //migrator.Update(); //Up
        }

        #endregion
    }
}