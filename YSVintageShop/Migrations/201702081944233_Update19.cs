namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update19 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Email = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                        PhoneNumber = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Files", "UserId", c => c.Int());
            CreateIndex("dbo.Files", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "Id");
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
