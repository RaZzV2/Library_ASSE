namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t111 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Editions", "BookType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Editions", "BookType");
        }
    }
}
