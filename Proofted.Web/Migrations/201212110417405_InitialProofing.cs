namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialProofing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.ProjectFiles",
                c => new
                    {
                        ProjectFileId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectFileId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Approvers",
                c => new
                    {
                        ApproverId = c.Int(nullable: false, identity: true),
                        Comments = c.Boolean(nullable: false),
                        Decision = c.Int(nullable: false),
                        Opened = c.Boolean(nullable: false),
                        Sent = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProjectFileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApproverId)
                .ForeignKey("dbo.ProjectFiles", t => t.ProjectFileId, cascadeDelete: true)
                .Index(t => t.ProjectFileId);
            
            CreateTable(
                "dbo.CommentThreads",
                c => new
                    {
                        CommentThreadId = c.Int(nullable: false, identity: true),
                        ThreadStatus = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProjectFileId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentThreadId)
                .ForeignKey("dbo.ProjectFiles", t => t.ProjectFileId, cascadeDelete: true)
                .Index(t => t.ProjectFileId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        CommentThreadId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.CommentThreads", t => t.CommentThreadId, cascadeDelete: true)
                .Index(t => t.CommentThreadId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.OrganizationGroups",
                c => new
                    {
                        OrganizationGroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        OrganizationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationGroupId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId, cascadeDelete: true)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.TagProjects",
                c => new
                    {
                        Tag_TagId = c.Int(nullable: false),
                        Project_ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagId, t.Project_ProjectId })
                .ForeignKey("dbo.Tags", t => t.Tag_TagId, cascadeDelete: false)
                .ForeignKey("dbo.Projects", t => t.Project_ProjectId, cascadeDelete: false)
                .Index(t => t.Tag_TagId)
                .Index(t => t.Project_ProjectId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TagProjects", new[] { "Project_ProjectId" });
            DropIndex("dbo.TagProjects", new[] { "Tag_TagId" });
            DropIndex("dbo.OrganizationGroups", new[] { "OrganizationId" });
            DropIndex("dbo.Tags", new[] { "OrganizationId" });
            DropIndex("dbo.Comments", new[] { "CommentThreadId" });
            DropIndex("dbo.CommentThreads", new[] { "ProjectFileId" });
            DropIndex("dbo.Approvers", new[] { "ProjectFileId" });
            DropIndex("dbo.ProjectFiles", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "OrganizationId" });
            DropForeignKey("dbo.TagProjects", "Project_ProjectId", "dbo.Projects");
            DropForeignKey("dbo.TagProjects", "Tag_TagId", "dbo.Tags");
            DropForeignKey("dbo.OrganizationGroups", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Tags", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Comments", "CommentThreadId", "dbo.CommentThreads");
            DropForeignKey("dbo.CommentThreads", "ProjectFileId", "dbo.ProjectFiles");
            DropForeignKey("dbo.Approvers", "ProjectFileId", "dbo.ProjectFiles");
            DropForeignKey("dbo.ProjectFiles", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "OrganizationId", "dbo.Organizations");
            DropTable("dbo.TagProjects");
            DropTable("dbo.OrganizationGroups");
            DropTable("dbo.Tags");
            DropTable("dbo.Comments");
            DropTable("dbo.CommentThreads");
            DropTable("dbo.Approvers");
            DropTable("dbo.ProjectFiles");
            DropTable("dbo.Projects");
            DropTable("dbo.Organizations");
        }
    }
}
