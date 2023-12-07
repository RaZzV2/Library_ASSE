namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t116 : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExtendedBorrows", "Borrow_Id", "dbo.Borrows");
            DropIndex("dbo.ExtendedBorrows", new[] { "Borrow_Id" });
            DropTable("dbo.ExtendedBorrows");
        }
    }
}
