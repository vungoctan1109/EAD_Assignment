namespace EAD_Assignment.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Title = c.String(),
                        Category = c.Int(nullable: false),
                        ImageUrl = c.String(),
                        Detail = c.String(),
                        Description = c.String(),
                        Author = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category, cascadeDelete: true)
                .Index(t => t.Category);
            
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
                        RemoveSelector = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "Category", "dbo.Categories");
            DropIndex("dbo.Articles", new[] { "Category" });
            DropTable("dbo.Sources");
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}
