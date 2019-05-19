namespace ReWork.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_offer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "OfferStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "OfferStatus");
        }
    }
}
