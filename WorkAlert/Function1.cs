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
using Microsoft.Extensions.Options;

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
        public async Task Run([TimerTrigger("0 */1 * * * *")] TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            try
            {
                log.LogInformation("Hello");
                var list = await _context.Users.AsNoTracking().Include(x => x.Calendar).ThenInclude(x => x.Works).ToListAsync();
                foreach(var u in list)
                {
                    if(u.Calendar.Works.Count > 0)
                    {
                        foreach (var w in u.Calendar.Works)
                        {
                            log.LogInformation($" Hello {u.Name} you have a work start at: {w.Start}");
                        }
                    }
                    else log.LogInformation($" Hello {u.Name} you don't have any {nameof(u.Calendar.Works)} today");
                }

                //Call Api
                //var config = new ConfigurationBuilder()
                //    .SetBasePath(context.FunctionAppDirectory)
                //    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                //    .AddEnvironmentVariables()
                //    .Build();
                //var setting1 = config["Setting1"];
                //var apiConnection = config.GetConnectionString("ApiConnection");

                //HttpClient client = new HttpClient();
                ////Get user has id = 1
                //var user = await (await client.GetAsync(apiConnection)).Content.ReadFromJsonAsync<User>();
            }
            catch (Exception ex)
            {
                log.LogInformation($" {ex.Message}");
            }


        }
    }
}
