namespace ReWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_job : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "Status");
        }
    }
}
