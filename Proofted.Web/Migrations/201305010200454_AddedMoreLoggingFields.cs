namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMoreLoggingFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Logs", "FileNo", c => c.Int(nullable: false));
            AddColumn("dbo.Logs", "Source", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Logs", "Source");
            DropColumn("dbo.Logs", "FileNo");
        }
    }
}
