﻿using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Threading.Tasks;

namespace Orleans.Example
{
    class Program
    {
        private const string ClusterId = "dev";
        private const string ServiceId = "OrleansSample";

        private const string Invariant = "System.Data.SqlClient";
        private const string ConnectionString = @"Data Source = WAKINGFORREST\SLEEPINGFOREST; Initial Catalog = Orleans; Integrated Security = True; Pooling = False; Max Pool Size = 200; MultipleActiveResultSets = True";

        static void Main(string[] args)
        {
            Console.Title = "Silo";

            RunMainAsync().Wait();

            Console.ReadKey();
        }

        private static async Task RunMainAsync()
        {
            try
            {
                var host = await InitialiseSilo();
                Console.WriteLine("Silo started successfully");
                Console.WriteLine("Press enter to exit...");
                Console.ReadLine();
                await host.StopAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task<ISiloHost> InitialiseSilo()
        {
            var builder = new SiloHostBuilder()
                // Membership
                .UseAdoNetClustering(options =>
                {
                    options.Invariant = Invariant;
                    options.ConnectionString = ConnectionString;
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = ClusterId;
                    options.ServiceId = ServiceId;
                })
                .ConfigureEndpoints(siloPort: 11111, gatewayPort: 30000)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(PersonGrain).Assembly).WithReferences())
                .ConfigureLogging(log => log.SetMinimumLevel(LogLevel.Warning).AddConsole());

            var host = builder.Build();
            await host.StartAsync();

            return host;
        }
    }
}
