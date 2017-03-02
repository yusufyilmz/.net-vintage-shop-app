namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserUserRelation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RelatedUserId = c.Int(nullable: false),
                        UserUserRelationType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserUserRelation");
        }
    }
}
