namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update16 : DbMigration
    {
        public override void Up()
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
            
            AddColumn("dbo.Files", "UserId", c => c.Int());
            CreateIndex("dbo.Files", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropIndex("dbo.Files", new[] { "UserId" });
            DropColumn("dbo.Files", "UserId");
            DropTable("dbo.Users");
        }
    }
}
