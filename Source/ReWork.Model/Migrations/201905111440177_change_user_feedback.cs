namespace ReWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_user_feedback : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FeedBacks", "CustomerProfileId", "dbo.CustomerProfiles");
            DropForeignKey("dbo.FeedBacks", "EmployeeProfileId", "dbo.EmployeeProfiles");
            DropForeignKey("dbo.FeedBacks", "JobId", "dbo.Jobs");
            DropIndex("dbo.FeedBacks", new[] { "JobId" });
            DropIndex("dbo.FeedBacks", new[] { "CustomerProfileId" });
            DropIndex("dbo.FeedBacks", new[] { "EmployeeProfileId" });
            AddColumn("dbo.FeedBacks", "SenderId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.FeedBacks", "ReceiverId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.FeedBacks", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.FeedBacks", "JobId", c => c.Int(nullable: false));
            CreateIndex("dbo.FeedBacks", "SenderId");
            CreateIndex("dbo.FeedBacks", "ReceiverId");
            CreateIndex("dbo.FeedBacks", "JobId");
            CreateIndex("dbo.FeedBacks", "User_Id");
            AddForeignKey("dbo.FeedBacks", "ReceiverId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FeedBacks", "SenderId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FeedBacks", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FeedBacks", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            DropColumn("dbo.FeedBacks", "CustomerProfileId");
            DropColumn("dbo.FeedBacks", "EmployeeProfileId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedBacks", "EmployeeProfileId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.FeedBacks", "CustomerProfileId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.FeedBacks", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.FeedBacks", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedBacks", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.FeedBacks", "ReceiverId", "dbo.AspNetUsers");
            DropIndex("dbo.FeedBacks", new[] { "User_Id" });
            DropIndex("dbo.FeedBacks", new[] { "JobId" });
            DropIndex("dbo.FeedBacks", new[] { "ReceiverId" });
            DropIndex("dbo.FeedBacks", new[] { "SenderId" });
            AlterColumn("dbo.FeedBacks", "JobId", c => c.Int());
            DropColumn("dbo.FeedBacks", "User_Id");
            DropColumn("dbo.FeedBacks", "ReceiverId");
            DropColumn("dbo.FeedBacks", "SenderId");
            CreateIndex("dbo.FeedBacks", "EmployeeProfileId");
            CreateIndex("dbo.FeedBacks", "CustomerProfileId");
            CreateIndex("dbo.FeedBacks", "JobId");
            AddForeignKey("dbo.FeedBacks", "JobId", "dbo.Jobs", "Id");
            AddForeignKey("dbo.FeedBacks", "EmployeeProfileId", "dbo.EmployeeProfiles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.FeedBacks", "CustomerProfileId", "dbo.CustomerProfiles", "Id", cascadeDelete: true);
        }
    }
}
