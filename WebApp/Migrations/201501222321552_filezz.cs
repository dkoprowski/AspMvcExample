namespace WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filezz : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductModels", "Path", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductModels", "Path", c => c.String(nullable: false));
        }
    }
}
