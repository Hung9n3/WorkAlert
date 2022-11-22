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
        //private readonly Context _context;
        public Function1()
        {
            //_context = context;
        }
        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            try
            {
                //var list = await _context.Users.Select(x => x.Id).Where(x => x == 1).FirstOrDefaultAsync();
                log.LogInformation("Hello");
                //Create config instance
                var config = new ConfigurationBuilder()
                    .SetBasePath(context.FunctionAppDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
                var setting1 = config["Setting1"];
                var defaultConnection = config.GetConnectionString("DefaultConnection");
                //Calling db

                //Calling api
                log.LogInformation($" ConnectionString: {defaultConnection}");
                HttpClient client = new HttpClient();
                var users = await (await client.GetAsync(defaultConnection)).Content.ReadFromJsonAsync<User>();
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
}
