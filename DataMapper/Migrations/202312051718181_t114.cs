namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t114 : DbMigration
    {
        public override void Up()
        {
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
                "dbo.Readers",
                c => new
                    {
                        ReaderId = c.Int(nullable: false, identity: true),
                        ReaderFirstName = c.String(),
                        ReaderLastName = c.String(),
                        Address = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.ReaderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Borrows", "Reader_ReaderId", "dbo.Readers");
            DropForeignKey("dbo.Borrows", "Edition_EditionId", "dbo.Editions");
            DropIndex("dbo.Borrows", new[] { "Reader_ReaderId" });
            DropIndex("dbo.Borrows", new[] { "Edition_EditionId" });
            DropTable("dbo.Readers");
            DropTable("dbo.Borrows");
        }
    }
}
