namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update23 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "OriginalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Products", "UsageStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "UsageStatus");
            DropColumn("dbo.Products", "OriginalPrice");
        }
    }
}
