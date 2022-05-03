namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_again : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Articles", "Category", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "Category" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Articles", "Category");
            AddForeignKey("dbo.Articles", "Category", "dbo.Categories", "Id", cascadeDelete: true);
        }
    }
}
