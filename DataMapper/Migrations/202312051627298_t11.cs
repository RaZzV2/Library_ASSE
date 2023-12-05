namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Editions",
                c => new
                    {
                        EditionId = c.Int(nullable: false, identity: true),
                        EditionName = c.String(),
                        EditionYear = c.Int(nullable: false),
                        PagesNumber = c.Int(nullable: false),
                        Book_BookId = c.Int(),
                    })
                .PrimaryKey(t => t.EditionId)
                .ForeignKey("dbo.Books", t => t.Book_BookId)
                .Index(t => t.Book_BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Editions", "Book_BookId", "dbo.Books");
            DropIndex("dbo.Editions", new[] { "Book_BookId" });
            DropTable("dbo.Editions");
        }
    }
}
