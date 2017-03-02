namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update18 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Files", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "UserId", c => c.Int());
        }
    }
}
