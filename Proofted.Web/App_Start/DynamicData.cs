namespace Proofted.Web.App_Start
{
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading;
    using System.Web.DynamicData;
    using System.Web.Routing;
    using System.Web.UI;

    using Proofted.Web.Filters;
    using Proofted.Web.Models;

    public class DynamicData
    {
        public static void Register(RouteCollection routes)
        {
            RegisterRoutes(routes);
            RegisterScripts();
        }

        private static InitializeSimpleMembershipAttribute.SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        public static void RegisterRoutes(RouteCollection routes)
        {
            //                    IMPORTANT: DATA MODEL REGISTRATION 
            // Uncomment this line to register an ADO.NET Entity Framework model for ASP.NET Dynamic Data.
            // Set ScaffoldAllTables = true only if you are sure that you want all tables in the
            // data model to support a scaffold (i.e. templates) view. To control scaffolding for
            // individual tables, create a partial class for the table and apply the
            // [ScaffoldTable(true)] attribute to the partial class.
            // Note: Make sure that you change "YourDataContextType" to the name of the data context
            // class in your application.
            // See http://go.microsoft.com/fwlink/?LinkId=257395 for more information on how to add and configure an Entity Data model to this project
            //DefaultModel.RegisterContext(typeof(ProoftedDbContext), new ContextConfiguration() { ScaffoldAllTables = true });

            //LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
            //UserDbContext.Borrow(context =>
            //{ context.UserProfiles.ToList(); });

            var contextConfiguration = new ContextConfiguration() { ScaffoldAllTables = true };

            var objectContextAdapter = (IObjectContextAdapter)new ProoftedDbContext();
            MvcApplication.DefaultModel.RegisterContext(() => objectContextAdapter.ObjectContext, contextConfiguration);

            // The following statement supports separate-page mode, where the List, Detail, Insert, and 
            // Update tasks are performed by using separate pages. To enable this mode, uncomment the following 
            // route definition, and comment out the route definitions in the combined-page mode section that follows.
            routes.Add(new DynamicDataRoute("{table}/{action}.aspx")
            {
                Constraints = new RouteValueDictionary(new { action = "List|Details|Edit|Insert" }),
                Model = MvcApplication.DefaultModel
            });

            // The following statements support combined-page mode, where the List, Detail, Insert, and
            // Update tasks are performed by using the same page. To enable this mode, uncomment the
            // following routes and comment out the route definition in the separate-page mode section above.
            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.List,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});

            //routes.Add(new DynamicDataRoute("{table}/ListDetails.aspx") {
            //    Action = PageAction.Details,
            //    ViewName = "ListDetails",
            //    Model = DefaultModel
            //});
        }

        private static void RegisterScripts()
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
            {
                Path = "~/Scripts/jquery-1.8.2.min.js",
                DebugPath = "~/Scripts/jquery-1.8.2.js",
                CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.min.js",
                CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.2.js",
                CdnSupportsSecureConnection = true,
                LoadSuccessExpression = "window.jQuery"
            });
        }
    }
}