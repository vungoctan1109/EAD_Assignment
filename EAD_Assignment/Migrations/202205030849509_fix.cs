namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Articles");
            AlterColumn("dbo.Articles", "Url", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Articles", "Url");
            DropColumn("dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articles", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Articles");
            AlterColumn("dbo.Articles", "Url", c => c.String());
            AddPrimaryKey("dbo.Articles", "Id");
        }
    }
}
