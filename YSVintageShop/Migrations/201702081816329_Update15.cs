namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update15 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "UserProductId");
            RenameColumn(table: "dbo.Products", name: "UserProductModel_Id", newName: "UserProductId");
            RenameIndex(table: "dbo.Products", name: "IX_UserProductModel_Id", newName: "IX_UserProductId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Products", name: "IX_UserProductId", newName: "IX_UserProductModel_Id");
            RenameColumn(table: "dbo.Products", name: "UserProductId", newName: "UserProductModel_Id");
            AddColumn("dbo.Products", "UserProductId", c => c.Int());
        }
    }
}
