namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Editions", "Book_BookId", "dbo.Books");
            DropForeignKey("dbo.Borrows", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.ExtendedBorrows", "Borrow_Id", "dbo.Borrows");
            DropForeignKey("dbo.Borrows", "Reader_ReaderId", "dbo.Readers");
            DropIndex("dbo.Editions", new[] { "Book_BookId" });
            DropIndex("dbo.Borrows", new[] { "Edition_EditionId" });
            DropIndex("dbo.Borrows", new[] { "Reader_ReaderId" });
            DropIndex("dbo.ExtendedBorrows", new[] { "Borrow_Id" });
            RenameColumn(table: "dbo.BookDomains", name: "BookDomain_BookDomainId", newName: "ParentDomain_BookDomainId");
            RenameIndex(table: "dbo.BookDomains", name: "IX_BookDomain_BookDomainId", newName: "IX_ParentDomain_BookDomainId");
            AddColumn("dbo.Authors", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Authors", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Students", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Students", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Students", "Age", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.BookDomains", "DomainName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Editions", "EditionName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Editions", "Book_BookId", c => c.Int(nullable: false));
            AlterColumn("dbo.Borrows", "Edition_EditionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Borrows", "Reader_ReaderId", c => c.Int(nullable: false));
            AlterColumn("dbo.ExtendedBorrows", "Borrow_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Readers", "ReaderFirstName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Readers", "ReaderLastName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Readers", "Address", c => c.String(nullable: false));
            CreateIndex("dbo.Editions", "Book_BookId");
            CreateIndex("dbo.Borrows", "Edition_EditionId");
            CreateIndex("dbo.Borrows", "Reader_ReaderId");
            CreateIndex("dbo.ExtendedBorrows", "Borrow_Id");
            AddForeignKey("dbo.Editions", "Book_BookId", "dbo.Books", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.Borrows", "Edition_EditionId", "dbo.Editions", "EditionId", cascadeDelete: true);
            AddForeignKey("dbo.ExtendedBorrows", "Borrow_Id", "dbo.Borrows", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Borrows", "Reader_ReaderId", "dbo.Readers", "ReaderId", cascadeDelete: true);
            DropColumn("dbo.Authors", "Name");
            DropColumn("dbo.Students", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Name", c => c.String());
            AddColumn("dbo.Authors", "Name", c => c.String());
            DropForeignKey("dbo.Borrows", "Reader_ReaderId", "dbo.Readers");
            DropForeignKey("dbo.ExtendedBorrows", "Borrow_Id", "dbo.Borrows");
            DropForeignKey("dbo.Borrows", "Edition_EditionId", "dbo.Editions");
            DropForeignKey("dbo.Editions", "Book_BookId", "dbo.Books");
            DropIndex("dbo.ExtendedBorrows", new[] { "Borrow_Id" });
            DropIndex("dbo.Borrows", new[] { "Reader_ReaderId" });
            DropIndex("dbo.Borrows", new[] { "Edition_EditionId" });
            DropIndex("dbo.Editions", new[] { "Book_BookId" });
            AlterColumn("dbo.Readers", "Address", c => c.String());
            AlterColumn("dbo.Readers", "ReaderLastName", c => c.String());
            AlterColumn("dbo.Readers", "ReaderFirstName", c => c.String());
            AlterColumn("dbo.ExtendedBorrows", "Borrow_Id", c => c.Int());
            AlterColumn("dbo.Borrows", "Reader_ReaderId", c => c.Int());
            AlterColumn("dbo.Borrows", "Edition_EditionId", c => c.Int());
            AlterColumn("dbo.Editions", "Book_BookId", c => c.Int());
            AlterColumn("dbo.Editions", "EditionName", c => c.String());
            AlterColumn("dbo.BookDomains", "DomainName", c => c.String());
            AlterColumn("dbo.Books", "Title", c => c.String());
            DropColumn("dbo.Students", "Age");
            DropColumn("dbo.Students", "LastName");
            DropColumn("dbo.Students", "FirstName");
            DropColumn("dbo.Authors", "LastName");
            DropColumn("dbo.Authors", "FirstName");
            RenameIndex(table: "dbo.BookDomains", name: "IX_ParentDomain_BookDomainId", newName: "IX_BookDomain_BookDomainId");
            RenameColumn(table: "dbo.BookDomains", name: "ParentDomain_BookDomainId", newName: "BookDomain_BookDomainId");
            CreateIndex("dbo.ExtendedBorrows", "Borrow_Id");
            CreateIndex("dbo.Borrows", "Reader_ReaderId");
            CreateIndex("dbo.Borrows", "Edition_EditionId");
            CreateIndex("dbo.Editions", "Book_BookId");
            AddForeignKey("dbo.Borrows", "Reader_ReaderId", "dbo.Readers", "ReaderId");
            AddForeignKey("dbo.ExtendedBorrows", "Borrow_Id", "dbo.Borrows", "Id");
            AddForeignKey("dbo.Borrows", "Edition_EditionId", "dbo.Editions", "EditionId");
            AddForeignKey("dbo.Editions", "Book_BookId", "dbo.Books", "BookId");
        }
    }
}
