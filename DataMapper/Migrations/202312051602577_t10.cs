namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookSubdomainBooks", "BookSubdomain_BookSubdomainId", "dbo.BookSubdomains");
            DropForeignKey("dbo.BookSubdomainBooks", "Book_BookId", "dbo.Books");
            DropIndex("dbo.BookSubdomains", new[] { "BookDomain_BookDomainId" });
            DropIndex("dbo.BookSubdomainBooks", new[] { "BookSubdomain_BookSubdomainId" });
            DropIndex("dbo.BookSubdomainBooks", new[] { "Book_BookId" });
            AddColumn("dbo.BookDomains", "BookDomain_BookDomainId", c => c.Int());
            CreateIndex("dbo.BookDomains", "BookDomain_BookDomainId");
            DropTable("dbo.BookSubdomains");
            DropTable("dbo.BookSubdomainBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookSubdomainBooks",
                c => new
                    {
                        BookSubdomain_BookSubdomainId = c.Int(nullable: false),
                        Book_BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookSubdomain_BookSubdomainId, t.Book_BookId });
            
            CreateTable(
                "dbo.BookSubdomains",
                c => new
                    {
                        BookSubdomainId = c.Int(nullable: false, identity: true),
                        SubdomainName = c.String(),
                        BookDomain_BookDomainId = c.Int(),
                    })
                .PrimaryKey(t => t.BookSubdomainId);
            
            DropIndex("dbo.BookDomains", new[] { "BookDomain_BookDomainId" });
            DropColumn("dbo.BookDomains", "BookDomain_BookDomainId");
            CreateIndex("dbo.BookSubdomainBooks", "Book_BookId");
            CreateIndex("dbo.BookSubdomainBooks", "BookSubdomain_BookSubdomainId");
            CreateIndex("dbo.BookSubdomains", "BookDomain_BookDomainId");
            AddForeignKey("dbo.BookSubdomainBooks", "Book_BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.BookSubdomainBooks", "BookSubdomain_BookSubdomainId", "dbo.BookSubdomains", "BookSubdomainId", cascadeDelete: true);
        }
    }
}
