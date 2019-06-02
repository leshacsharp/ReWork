namespace ReWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit_user : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Status", c => c.Int(nullable: false));
        }
    }
}
