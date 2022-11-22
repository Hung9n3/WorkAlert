using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;

namespace WorkAlert
{
    public class Function1
    {
        private readonly Context _context;

        public Function1(Context context)
        {
            _context = context;
        }
        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            try
            {
                //var list = await _context.Users.Select(x => x.Id).Where(x => x == 1).FirstOrDefaultAsync();
                log.LogInformation("Hello");
                //Create config instance
                //var config = new ConfigurationBuilder()
                //    .SetBasePath(context.FunctionAppDirectory)
                //    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                //    .AddEnvironmentVariables()
                //    .Build();
                //var setting1 = config["Setting1"];
                string defaultConnection = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");
                //Calling db

                //Calling api
                //var defaultConnection = config.GetConnectionString("DefaultConnection");
                log.LogInformation($" ConnectionString: {defaultConnection}");
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(defaultConnection);
                var users = await response.Content.ReadFromJsonAsync<User>();
                foreach (var w in users.Calendar.Works)
                {
                    log.LogInformation($" Hello {users.Name} you have a work start at: {w.Start}");
                }
            }
            catch(Exception ex)
            {
                log.LogInformation($" {ex.Message}");
            }


        }
    }
    //public class ContextFactory : IDesignTimeDbContextFactory<Context>
    //{
    //    public Context CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<Context>();
    //        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings:SqlConnection"));
    //        return new Context(optionsBuilder.Options);
    //    }
    //}
}
