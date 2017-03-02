namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UserId" });
            AlterColumn("dbo.UserProductRelation", "UserId", c => c.String());
            AlterColumn("dbo.UserProductRelation", "RelatedProductId", c => c.String());
            AlterColumn("dbo.UserUserRelation", "UserId", c => c.String());
            AlterColumn("dbo.UserUserRelation", "RelatedUserId", c => c.String());
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.UserId);
            
            AlterColumn("dbo.UserUserRelation", "RelatedUserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserUserRelation", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserProductRelation", "RelatedProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.UserProductRelation", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Files", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "UserId");
        }
    }
}
