namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MainHeads",
                c => new
                    {
                        MainHeadId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Slug = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MainHeadId);
            
            AddColumn("dbo.Files", "MainHeadModel_MainHeadId", c => c.Int());
            CreateIndex("dbo.Files", "MainHeadModel_MainHeadId");
            AddForeignKey("dbo.Files", "MainHeadModel_MainHeadId", "dbo.MainHeads", "MainHeadId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "MainHeadModel_MainHeadId", "dbo.MainHeads");
            DropIndex("dbo.Files", new[] { "MainHeadModel_MainHeadId" });
            DropColumn("dbo.Files", "MainHeadModel_MainHeadId");
            DropTable("dbo.MainHeads");
        }
    }
}
