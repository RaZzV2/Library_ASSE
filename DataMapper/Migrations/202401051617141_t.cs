// <copyright file="202401051617141_t.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Represents a database migration to create tables for Authors, Books, BookDomains, and Editions.
    /// </summary>
    public partial class T : DbMigration
    {
        /// <summary>
        /// Applies changes to the database schema during the migration.
        /// </summary>
        public override void Up()
        {
            this.CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.BookId);

            this.CreateTable(
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

            this.CreateTable(
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

            this.CreateTable(
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

            this.CreateTable(
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

            this.CreateTable(
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

            this.CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
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

            this.CreateTable(
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

        /// <inheritdoc/>
        public override void Down()
        {
            this.DropForeignKey("dbo.Borrows", "Reader_ReaderId", "dbo.Readers");
            this.DropForeignKey("dbo.ExtendedBorrows", "Borrow_Id", "dbo.Borrows");
            this.DropForeignKey("dbo.Borrows", "Edition_EditionId", "dbo.Editions");
            this.DropForeignKey("dbo.Editions", "Book_BookId", "dbo.Books");
            this.DropForeignKey("dbo.BookDomains", "BookDomain_BookDomainId", "dbo.BookDomains");
            this.DropForeignKey("dbo.BookDomainBooks", "Book_BookId", "dbo.Books");
            this.DropForeignKey("dbo.BookDomainBooks", "BookDomain_BookDomainId", "dbo.BookDomains");
            this.DropForeignKey("dbo.BookAuthors", "Author_Id", "dbo.Authors");
            this.DropForeignKey("dbo.BookAuthors", "Book_BookId", "dbo.Books");
            this.DropIndex("dbo.BookDomainBooks", new[] { "Book_BookId" });
            this.DropIndex("dbo.BookDomainBooks", new[] { "BookDomain_BookDomainId" });
            this.DropIndex("dbo.BookAuthors", new[] { "Author_Id" });
            this.DropIndex("dbo.BookAuthors", new[] { "Book_BookId" });
            this.DropIndex("dbo.ExtendedBorrows", new[] { "Borrow_Id" });
            this.DropIndex("dbo.Borrows", new[] { "Reader_ReaderId" });
            this.DropIndex("dbo.Borrows", new[] { "Edition_EditionId" });
            this.DropIndex("dbo.Editions", new[] { "Book_BookId" });
            this.DropIndex("dbo.BookDomains", new[] { "BookDomain_BookDomainId" });
            this.DropTable("dbo.BookDomainBooks");
            this.DropTable("dbo.BookAuthors");
            this.DropTable("dbo.Students");
            this.DropTable("dbo.Readers");
            this.DropTable("dbo.ExtendedBorrows");
            this.DropTable("dbo.Borrows");
            this.DropTable("dbo.Editions");
            this.DropTable("dbo.BookDomains");
            this.DropTable("dbo.Books");
            this.DropTable("dbo.Authors");
        }
    }
}
