using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace SW_API.Infrastructure.Context
{
    /// <summary>
    /// Mediator tenant context factory
    /// </summary>
    class SWDbContextFactory : IDesignTimeDbContextFactory<SWDbContext>
    {
        /// <summary>
        /// Create data base context
        /// </summary>
        /// <param name="args">Arguments</param>
        public SWDbContext CreateDbContext(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()

                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../SW_API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<SWDbContext>();

            var connectionString = config.GetConnectionString("Database");

            optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("SW_API.Infrastructure"));

            return new SWDbContext(optionsBuilder.Options);
        }
    }
}
