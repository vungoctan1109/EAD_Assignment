namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_subLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "SubLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "SubLink");
        }
    }
}
