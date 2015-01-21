namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstBaseModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        DateOfPublication = c.DateTime(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.ProductModels", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false),
                        ReleaseDate = c.DateTime(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentModels", "ProductId", "dbo.ProductModels");
            DropForeignKey("dbo.CommentModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "ProductId" });
            DropIndex("dbo.CommentModels", new[] { "ApplicationUserId" });
            DropTable("dbo.ProductModels");
            DropTable("dbo.CommentModels");
        }
    }
}
