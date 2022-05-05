namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Url = c.String(nullable: false, maxLength: 128),
                        Title = c.String(),
                        ImageUrl = c.String(),
                        Detail = c.String(),
                        Description = c.String(),
                        Author = c.String(),
                        CreatedAt = c.DateTime(),
                        UpdatedAt = c.DateTime(),
                        CategoryId = c.Int(nullable: false),
                        Status = c.Int(),
                    })
                .PrimaryKey(t => t.Url);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Link = c.String(),
                        LinkSelector = c.String(),
                        TitleDetailSelector = c.String(),
                        ContentDetailSelector = c.String(),
                        ImageDetailSelector = c.String(),
                        DescriptionDetailSelector = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Sources");
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}
