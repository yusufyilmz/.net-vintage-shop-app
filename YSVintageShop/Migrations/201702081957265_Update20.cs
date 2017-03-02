namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update20 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserUserId", c => c.Int());
            AddColumn("dbo.Users", "UserUserModel_Id", c => c.Int());
            CreateIndex("dbo.Users", "UserUserId");
            CreateIndex("dbo.Users", "UserUserModel_Id");
            AddForeignKey("dbo.Users", "UserUserId", "dbo.UserProductRelation", "Id");
            AddForeignKey("dbo.Users", "UserUserModel_Id", "dbo.UserUserRelation", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "UserUserModel_Id", "dbo.UserUserRelation");
            DropForeignKey("dbo.Users", "UserUserId", "dbo.UserProductRelation");
            DropIndex("dbo.Users", new[] { "UserUserModel_Id" });
            DropIndex("dbo.Users", new[] { "UserUserId" });
            DropColumn("dbo.Users", "UserUserModel_Id");
            DropColumn("dbo.Users", "UserUserId");
        }
    }
}
