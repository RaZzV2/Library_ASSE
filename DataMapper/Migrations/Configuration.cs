namespace DataMapper.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// The <see cref="Configuration"/> class represents the configuration for Entity Framework Migrations.
    /// It controls how migrations are applied to the database and provides a method for seeding initial data.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<Library.Models.LibraryContext>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// Configures the behavior of database migrations.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        /// <summary>
        /// This method will be called after migrating to the latest version.
        /// Use the DbSet<T>.AddOrUpdate() helper extension method to avoid creating duplicate seed data.
        /// </summary>
        /// <param name="context">The <see cref="Library.Models.LibraryContext"/> instance representing the database context.</param>
        protected override void Seed(Library.Models.LibraryContext context)
        {
            // This method will be called after migrating to the latest version.

            // You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
