namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WhoKnows1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganizationUsers", "Organization_OrganizationId", "dbo.Organizations");
            DropIndex("dbo.OrganizationUsers", new[] { "Organization_OrganizationId" });
            RenameColumn(table: "dbo.OrganizationUsers", name: "Organization_OrganizationId", newName: "OrganizationId");
            AddForeignKey("dbo.OrganizationUsers", "OrganizationId", "dbo.Organizations", "OrganizationId", cascadeDelete: true);
            CreateIndex("dbo.OrganizationUsers", "OrganizationId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrganizationUsers", new[] { "OrganizationId" });
            DropForeignKey("dbo.OrganizationUsers", "OrganizationId", "dbo.Organizations");
            RenameColumn(table: "dbo.OrganizationUsers", name: "OrganizationId", newName: "Organization_OrganizationId");
            CreateIndex("dbo.OrganizationUsers", "Organization_OrganizationId");
            AddForeignKey("dbo.OrganizationUsers", "Organization_OrganizationId", "dbo.Organizations", "OrganizationId");
        }
    }
}
