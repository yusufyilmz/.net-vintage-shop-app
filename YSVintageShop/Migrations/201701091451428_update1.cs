namespace YSVintageShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Slug = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        BrandId = c.Int(),
                        CollectionId = c.Int(),
                        ProductId = c.Int(),
                        MainCollectionModel_MainCollectionId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .ForeignKey("dbo.Collections", t => t.CollectionId)
                .ForeignKey("dbo.MainCollections", t => t.MainCollectionModel_MainCollectionId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.BrandId)
                .Index(t => t.CollectionId)
                .Index(t => t.ProductId)
                .Index(t => t.MainCollectionModel_MainCollectionId);
            
            CreateTable(
                "dbo.Collections",
                c => new
                    {
                        CollectionId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Slug = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        MainCollectionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CollectionId)
                .ForeignKey("dbo.MainCollections", t => t.MainCollectionId, cascadeDelete: true)
                .Index(t => t.MainCollectionId);
            
            CreateTable(
                "dbo.MainCollections",
                c => new
                    {
                        MainCollectionId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Slug = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MainCollectionId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Description = c.String(maxLength: 100),
                        Details = c.String(maxLength: 100),
                        Slug = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Stock = c.Int(nullable: false),
                        State = c.String(maxLength: 30),
                        BrandId = c.Int(nullable: false),
                        CollectionId = c.Int(nullable: false),
                        UserProductId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.Collections", t => t.CollectionId, cascadeDelete: true)
                .ForeignKey("dbo.UserProducts", t => t.UserProductId)
                .Index(t => t.BrandId)
                .Index(t => t.CollectionId)
                .Index(t => t.UserProductId);
            
            CreateTable(
                "dbo.UserProducts",
                c => new
                    {
                        UserProductId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ProductState = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.UserProductId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Products", "UserProductId", "dbo.UserProducts");
            DropForeignKey("dbo.Files", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CollectionId", "dbo.Collections");
            DropForeignKey("dbo.Products", "BrandId", "dbo.Brands");
            DropForeignKey("dbo.Files", "MainCollectionModel_MainCollectionId", "dbo.MainCollections");
            DropForeignKey("dbo.Collections", "MainCollectionId", "dbo.MainCollections");
            DropForeignKey("dbo.Files", "CollectionId", "dbo.Collections");
            DropForeignKey("dbo.Files", "BrandId", "dbo.Brands");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Products", new[] { "UserProductId" });
            DropIndex("dbo.Products", new[] { "CollectionId" });
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Collections", new[] { "MainCollectionId" });
            DropIndex("dbo.Files", new[] { "MainCollectionModel_MainCollectionId" });
            DropIndex("dbo.Files", new[] { "ProductId" });
            DropIndex("dbo.Files", new[] { "CollectionId" });
            DropIndex("dbo.Files", new[] { "BrandId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.UserProducts");
            DropTable("dbo.Products");
            DropTable("dbo.MainCollections");
            DropTable("dbo.Collections");
            DropTable("dbo.Files");
            DropTable("dbo.Brands");
        }
    }
}
