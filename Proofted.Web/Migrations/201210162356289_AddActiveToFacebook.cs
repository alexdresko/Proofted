namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddActiveToFacebook : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FaceBookAppCredentials", "Active", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FaceBookAppCredentials", "Active");
        }
    }
}
