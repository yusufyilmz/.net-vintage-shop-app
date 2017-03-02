namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update14 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "UserProductId", "dbo.UserProducts");
            DropIndex("dbo.Products", new[] { "UserProductId" });
            DropTable("dbo.UserProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        UserProductId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductState = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserProductId);
            
            CreateIndex("dbo.Products", "UserProductId");
            AddForeignKey("dbo.Products", "UserProductId", "dbo.UserProducts", "UserProductId");
        }
    }
}
