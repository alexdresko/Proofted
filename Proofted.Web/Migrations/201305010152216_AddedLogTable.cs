namespace Proofted.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLogTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        CreatedDateTime = c.DateTime(nullable: false),
                        Details = c.String(),
                        LogLevel = c.Int(nullable: false),
                        LogType = c.Int(nullable: false),
                        Message = c.String(),
                        MessageId = c.Int(nullable: false),
                        Module = c.String(),
                        StackTrace = c.String(),
                        User = c.String(),
                    })
                .PrimaryKey(t => t.LogId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Logs");
        }
    }
}
