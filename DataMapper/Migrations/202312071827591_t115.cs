namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class t115 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Readers", "Role", c => c.Boolean(nullable: false));
            AddColumn("dbo.Readers", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Readers", "PhoneNumber");
            DropColumn("dbo.Readers", "Role");
        }
    }
}
