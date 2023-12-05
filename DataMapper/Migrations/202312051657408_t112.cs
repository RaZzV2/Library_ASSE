namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t112 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Editions", "BorrowableBooks", c => c.Int(nullable: false));
            AddColumn("dbo.Editions", "UnBorrowableBooks", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Editions", "UnBorrowableBooks");
            DropColumn("dbo.Editions", "BorrowableBooks");
        }
    }
}
