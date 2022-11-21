using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WorkAlert
{
    public class Function1
    {
        [FunctionName("Function1")]
        public async Task Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log, ExecutionContext context)
        {
            try
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(context.FunctionAppDirectory)
                    .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();
                var defaultConnection = config.GetConnectionString("DefaultConnection");
                var setting1 = config["Setting1"];
                //string defaultConnection = Environment.GetEnvironmentVariable("ConnectionStrings:DefaultConnection");
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
}
