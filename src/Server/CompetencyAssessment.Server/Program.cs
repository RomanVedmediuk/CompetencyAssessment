using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetencyAssessment.Server
{
    using Azure.Identity;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    if (hostingContext.HostingEnvironment.IsDevelopment())
                    {
                        var connectionString = settings["AzureAppConfigConnectionString"];
                        config.AddAzureAppConfiguration(connectionString);
                    }
                    else
                    {
                        var endpoint = settings["AppConfigEndpoint"];
                        var credentials = new ManagedIdentityCredential();
                        config.AddAzureAppConfiguration(options =>
                        {
                            options.Connect(new Uri(endpoint), credentials);
                        });
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
