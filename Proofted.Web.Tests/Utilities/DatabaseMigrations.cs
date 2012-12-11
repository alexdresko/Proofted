using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proofted.Web.Tests.Utilities
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Proofted.Web.Filters;
    using Proofted.Web.Migrations;
    using Proofted.Web.Models;
    using Proofted.Web.Models.Security;

    class DatabaseMigrations
    {
        public static DbMigrator GetMigrator(TestContext testContext)
        {

            var configuration = new Configuration();
            configuration.TargetDatabase = new DbConnectionInfo("DefaultConnection");
            var migrator = new DbMigrator(configuration);
            return migrator;
        }

        public static void SetDataDirectory(TestContext testContext)
        {
            var dataDirectory = testContext.TestDir;
            if (!Directory.Exists(dataDirectory))
            {
                Directory.CreateDirectory(dataDirectory);
            }

            AppDomain domain = AppDomain.CurrentDomain;
            domain.SetData("DataDirectory", dataDirectory);
        }

        public static void DeleteDatabase(TestContext testContext)
        {
            if (Database.Exists("DefaultConnection"))
            {
                try
                {
                    Database.Delete("DefaultConnection");
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
            SetDataDirectory(testContext);
        }

        public static void UpdateDatabase(TestContext testContext, string targetMigration = null)
        {

            var migrator = GetMigrator(testContext);
            if (targetMigration != null)
            {
                migrator.Update(targetMigration);
            }
            else
            {
                migrator.Update();
            }
        }

        public static void DeleteAndUpgradeDatabase(TestContext testContext)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", @"d:\temp\");
            DeleteDatabase(testContext);
            SetDataDirectory(testContext);
            SetupSecurity(testContext);
            
            UpdateDatabase(testContext);
        }
        
        private static InitializeSimpleMembershipAttribute.SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        private static void SetupSecurity(TestContext testContext)
        {
            var security = new WebSecurityWrapper();


            security.InitializeDatabaseConnection(
                "DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);

            SetDataDirectory(testContext);

            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);

            //UserDbContext.Borrow(context =>
            //    { context.UserProfiles.ToList(); });
        }
    }
}
