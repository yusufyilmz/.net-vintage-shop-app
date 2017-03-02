namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "State");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "State", c => c.String(maxLength: 30));
        }
    }
}
