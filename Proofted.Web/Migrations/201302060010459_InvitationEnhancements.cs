namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvitationEnhancements : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invitations", "Guid", c => c.String());
            AddColumn("dbo.Invitations", "Used", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invitations", "Used");
            DropColumn("dbo.Invitations", "Guid");
        }
    }
}
