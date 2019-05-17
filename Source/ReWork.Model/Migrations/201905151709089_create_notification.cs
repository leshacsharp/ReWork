namespace ReWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_notification : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 100),
                        AddedDate = c.DateTime(nullable: false),
                        SenderId = c.String(nullable: false, maxLength: 128),
                        ReciverId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ReciverId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReciverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Notifications", "ReciverId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "ReciverId" });
            DropIndex("dbo.Notifications", new[] { "SenderId" });
            DropTable("dbo.Notifications");
        }
    }
}
