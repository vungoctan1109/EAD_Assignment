namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_again3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "Category", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Category", c => c.Int(nullable: false));
        }
    }
}
