using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace DAL
{
    public class Program
    {
        public static void Main(string[] args)
        {

            using (DataTable providers = DbProviderFactories.GetFactoryClasses())
            {
                var prov = providers.Rows;
            }

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
