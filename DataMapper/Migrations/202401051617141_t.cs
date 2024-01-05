namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t : DbMigration
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
                        BookDomain_BookDomainId = c.Int(),
                    })
                .PrimaryKey(t => t.BookDomainId)
                .ForeignKey("dbo.BookDomains", t => t.BookDomain_BookDomainId)
                .Index(t => t.BookDomain_BookDomainId);
            
            CreateTable(
                "dbo.Editions",
                c => new
                    {
                        EditionId = c.Int(nullable: false, identity: true),
                        EditionName = c.String(),
                        EditionYear = c.Int(nullable: false),
                        PagesNumber = c.Int(nullable: false),
                        BorrowableBooks = c.Int(nullable: false),
                        UnBorrowableBooks = c.Int(nullable: false),
                        BookType = c.Int(nullable: false),
                        Book_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.EditionId)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .Index(t => t.Book_BookId);
            
            CreateTable(
                "dbo.Borrows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BorrowStartDate = c.DateTime(nullable: false),
                        BorrowEndDate = c.DateTime(nullable: false),
                        IsReturned = c.Boolean(nullable: false),
                        Edition_EditionId = c.Int(),
                        Reader_ReaderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Editions", t => t.Edition_EditionId)
                .ForeignKey("dbo.Readers", t => t.Reader_ReaderId)
                .Index(t => t.Edition_EditionId)
                .Index(t => t.Reader_ReaderId);
            
            CreateTable(
                "dbo.ExtendedBorrows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Borrow_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Borrows", t => t.Borrow_Id)
                .Index(t => t.Borrow_Id);
            
            CreateTable(
                "dbo.Readers",
                c => new
                    {
                        ReaderId = c.Int(nullable: false, identity: true),
                        ReaderFirstName = c.String(),
                        ReaderLastName = c.String(),
                        Address = c.String(),
                        EmailAddress = c.String(),
                        Role = c.Boolean(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.ReaderId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrows", "Reader_ReaderId", "dbo.Readers");
            DropForeignKey("dbo.ExtendedBorrows", "Borrow_Id", "dbo.Borrows");
            DropForeignKey("dbo.Borrows", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.Editions", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookDomains", "BookDomain_BookDomainId", "dbo.BookDomains");
            DropForeignKey("dbo.BookDomainBooks", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.BookDomainBooks", "BookDomain_BookDomainId", "dbo.BookDomains");
            DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books");
            DropIndex("dbo.BookDomainBooks", new[] { "Book_BookId" });
            DropIndex("dbo.BookDomainBooks", new[] { "BookDomain_BookDomainId" });
            DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            DropIndex("dbo.BookAuthors", new[] { "Book_BookId" });
            DropIndex("dbo.ExtendedBorrows", new[] { "Borrow_Id" });
            DropIndex("dbo.Borrows", new[] { "Reader_ReaderId" });
            DropIndex("dbo.Borrows", new[] { "Edition_EditionId" });
            DropIndex("dbo.Editions", new[] { "Book_BookId" });
            DropIndex("dbo.BookDomains", new[] { "BookDomain_BookDomainId" });
            DropTable("dbo.BookDomainBooks");
            DropTable("dbo.BookAuthors");
            DropTable("dbo.Students");
            DropTable("dbo.Readers");
            DropTable("dbo.ExtendedBorrows");
            DropTable("dbo.Borrows");
            DropTable("dbo.Editions");
            DropTable("dbo.BookDomains");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
