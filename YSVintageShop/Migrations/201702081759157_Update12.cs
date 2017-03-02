namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update12 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProductRelation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RelatedProductId = c.Int(nullable: false),
                        UserProductRelationType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserProductRelation");
        }
    }
}
