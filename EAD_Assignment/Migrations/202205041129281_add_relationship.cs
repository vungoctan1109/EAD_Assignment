namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_relationship : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Articles", "Category");
            AddForeignKey("dbo.Articles", "Category", "dbo.Categories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Category", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "Category" });
        }
    }
}
