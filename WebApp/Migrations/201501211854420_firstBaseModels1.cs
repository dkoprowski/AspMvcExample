namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstBaseModels1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CommentModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "ApplicationUserId" });
            CreateIndex("dbo.CommentModels", "ApplicationUserId");
            AddForeignKey("dbo.CommentModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CommentModels", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.CommentModels", new[] { "ApplicationUserId" });
            CreateIndex("dbo.CommentModels", "ApplicationUserId");
            AddForeignKey("dbo.CommentModels", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
    }
}
