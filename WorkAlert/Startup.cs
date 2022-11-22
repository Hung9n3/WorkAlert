using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(WorkAlert.Startup))]

namespace WorkAlert
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("ConnectionStrings:SqlConnection");
            ExecutionContext context = new();
            //var config = new ConfigurationBuilder()
            //        .SetBasePath(context.FunctionAppDirectory)
            //        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
            //        .AddEnvironmentVariables()
            //        .Build();
            //var defaultConnection = config.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<Context>(
                options => options.UseSqlServer(SqlConnection));
        }
    }
}

 

