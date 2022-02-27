using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace my_books
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {

            Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("Logs/log.txt",rollingInterval:RollingInterval.Day)
                    .CreateLogger();
            CreateHostBuilder(args).Build().Run();
            }
            finally
            {

                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
