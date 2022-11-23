using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace WorkAlert
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Calendar> Calendars { get; set; } = null!;
        public virtual DbSet<Work> Works { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
    //public class ContextFactory : IDesignTimeDbContextFactory<Context>
    //{
    //    public Context CreateDbContext(string[] args)
    //    {
    //        ExecutionContext context = new();
    //        var config = new ConfigurationBuilder()
    //                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
    //                .AddEnvironmentVariables()
    //                .Build();
    //        string SqlConnection = config.GetConnectionString("LocalConnection");

            
    //        var optionsBuilder = new DbContextOptionsBuilder<Context>();
    //        optionsBuilder.UseSqlServer(SqlConnection);

    //        return new Context(optionsBuilder.Options);
    //    }
    //}
}
