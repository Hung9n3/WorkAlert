using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

[assembly: FunctionsStartup(typeof(WorkAlert.Startup))]

namespace WorkAlert
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {

            ExecutionContext context = new();
            var config = new ConfigurationBuilder()
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
            string SqlConnection = config.GetConnectionString("SqlConnection");

            builder.Services.AddDbContext<Context>(
                options => options.UseSqlServer(SqlConnection),
                ServiceLifetime.Transient
                );
        }
    }
}

 

