namespace ReWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "PriceDiscussed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Jobs", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Price", c => c.Int());
            DropColumn("dbo.Jobs", "PriceDiscussed");
        }
    }
}
