namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix_again2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Articles", "CreatedAt", c => c.DateTime());
            AlterColumn("dbo.Articles", "UpdatedAt", c => c.DateTime());
            AlterColumn("dbo.Articles", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Articles", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Articles", "UpdatedAt", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Articles", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
