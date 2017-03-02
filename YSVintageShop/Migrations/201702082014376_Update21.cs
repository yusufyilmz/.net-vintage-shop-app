namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update21 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserProductRelation", "RelatedProductId");
            DropColumn("dbo.UserUserRelation", "RelatedUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserUserRelation", "RelatedUserId", c => c.String());
            AddColumn("dbo.UserProductRelation", "RelatedProductId", c => c.String());
        }
    }
}
