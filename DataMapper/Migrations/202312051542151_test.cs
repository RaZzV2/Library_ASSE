namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.BookDomains",
                c => new
                    {
                        BookDomainId = c.Int(nullable: false, identity: true),
                        DomainName = c.String(),
                    })
                .PrimaryKey(t => t.BookDomainId);
            
            CreateTable(
                "dbo.BookSubdomains",
                c => new
                    {
                        BookSubdomainId = c.Int(nullable: false, identity: true),
                        SubdomainName = c.String(),
                        BookDomain_BookDomainId = c.Int(),
                    })
                .PrimaryKey(t => t.BookSubdomainId)
                .ForeignKey("dbo.BookDomains", t => t.BookDomain_BookDomainId)
                .Index(t => t.BookDomain_BookDomainId);
            
            CreateTable(
                "dbo.BookAuthors",
                c => new
                    {
                        Book_BookId = c.Int(nullable: false),
                        Author_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_BookId, t.Author_Id })
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .Index(t => t.Book_BookId)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.BookDomainBooks",
                c => new
                    {
                        BookDomain_BookDomainId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookDomain_BookDomainId, t.Book_BookId })
                .ForeignKey("dbo.BookDomains", t => t.BookDomain_BookDomainId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.BookDomain_BookDomainId)
                .Index(t => t.Book_BookId);
            
            CreateTable(
                "dbo.BookSubdomainBooks",
                c => new
                    {
                        BookSubdomain_BookSubdomainId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookSubdomain_BookSubdomainId, t.Book_BookId })
                .ForeignKey("dbo.BookSubdomains", t => t.BookSubdomain_BookSubdomainId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_BookId, cascadeDelete: true)
                .Index(t => t.BookSubdomain_BookSubdomainId)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookSubdomainBooks", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookSubdomainBooks", "BookSubdomain_BookSubdomainId", "dbo.BookSubdomains");
            DropForeignKey("dbo.BookSubdomains", "BookDomain_BookDomainId", "dbo.BookDomains");
            DropForeignKey("dbo.BookDomainBooks", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookDomainBooks", "BookDomain_BookDomainId", "dbo.BookDomains");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books");
            DropIndex("dbo.BookSubdomainBooks", new[] { "Book_BookId" });
            DropIndex("dbo.BookSubdomainBooks", new[] { "BookSubdomain_BookSubdomainId" });
            DropIndex("dbo.BookDomainBooks", new[] { "Book_BookId" });
            DropIndex("dbo.BookDomainBooks", new[] { "BookDomain_BookDomainId" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_BookId" });
            DropIndex("dbo.BookSubdomains", new[] { "BookDomain_BookDomainId" });
            DropTable("dbo.BookSubdomainBooks");
            DropTable("dbo.BookDomainBooks");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.BookSubdomains");
            DropTable("dbo.BookDomains");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
