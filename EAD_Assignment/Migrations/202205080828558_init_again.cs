namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_again : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sources", "Link", c => c.String(nullable: false));
            AlterColumn("dbo.Sources", "LinkSelector", c => c.String(nullable: false));
            AlterColumn("dbo.Sources", "TitleDetailSelector", c => c.String(nullable: false));
            AlterColumn("dbo.Sources", "ContentDetailSelector", c => c.String(nullable: false));
            AlterColumn("dbo.Sources", "ImageDetailSelector", c => c.String(nullable: false));
            AlterColumn("dbo.Sources", "DescriptionDetailSelector", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sources", "DescriptionDetailSelector", c => c.String());
            AlterColumn("dbo.Sources", "ImageDetailSelector", c => c.String());
            AlterColumn("dbo.Sources", "ContentDetailSelector", c => c.String());
            AlterColumn("dbo.Sources", "TitleDetailSelector", c => c.String());
            AlterColumn("dbo.Sources", "LinkSelector", c => c.String());
            AlterColumn("dbo.Sources", "Link", c => c.String());
        }
    }
}
