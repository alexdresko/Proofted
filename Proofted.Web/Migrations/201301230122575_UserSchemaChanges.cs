namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserSchemaChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrganizationGroups", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.OrganizationGroups", new[] { "OrganizationId" });
            CreateTable(
                "dbo.OrganizationUsers",
                c => new
                    {
                        OrganizationUserId = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Organization_OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.OrganizationUserId)
                .ForeignKey("dbo.Organizations", t => t.Organization_OrganizationId)
                .Index(t => t.Organization_OrganizationId);
            
            DropTable("dbo.OrganizationGroups");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OrganizationGroups",
                c => new
                    {
                        OrganizationGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationGroupId);
            
            DropIndex("dbo.OrganizationUsers", new[] { "Organization_OrganizationId" });
            DropForeignKey("dbo.OrganizationUsers", "Organization_OrganizationId", "dbo.Organizations");
            DropTable("dbo.OrganizationUsers");
            CreateIndex("dbo.OrganizationGroups", "OrganizationId");
            AddForeignKey("dbo.OrganizationGroups", "OrganizationId", "dbo.Organizations", "OrganizationId", cascadeDelete: true);
        }
    }
}
