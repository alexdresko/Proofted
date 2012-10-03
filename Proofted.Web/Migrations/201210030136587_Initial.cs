namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaceBookAppCredentials",
                c => new
                    {
                        FaceBookAppCredentialId = c.Int(nullable: false, identity: true),
                        AppId = c.String(),
                        SecretKey = c.String(),
                    })
                .PrimaryKey(t => t.FaceBookAppCredentialId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FaceBookAppCredentials");
        }
    }
}
