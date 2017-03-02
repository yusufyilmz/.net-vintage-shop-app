namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "UserProductModel_Id", c => c.Int());
            CreateIndex("dbo.Products", "UserProductModel_Id");
            AddForeignKey("dbo.Products", "UserProductModel_Id", "dbo.UserProductRelation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "UserProductModel_Id", "dbo.UserProductRelation");
            DropIndex("dbo.Products", new[] { "UserProductModel_Id" });
            DropColumn("dbo.Products", "UserProductModel_Id");
        }
    }
}
