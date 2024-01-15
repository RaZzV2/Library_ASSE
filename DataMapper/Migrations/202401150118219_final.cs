// <copyright file="202401150118219_final.cs" company="Transilvania University Of Brasov">
// Dragomir Razvan
// </copyright>

namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    /// <summary>
    /// Applies changes to the database schema during the migration.
    /// </summary>
    public partial class Final : DbMigration
    {
        /// <inheritdoc/>
        public override void Up()
        {
            this.DropTable("dbo.Students");
        }

        /// <inheritdoc/>
        public override void Down()
        {
            this.CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
    }
}
