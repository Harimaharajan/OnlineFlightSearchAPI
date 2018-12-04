using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using OnlineFlightSearchAPI.FlightServices;
using Unity;
using Unity.Lifetime;

namespace OnlineFlightSearchAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

            IUnityContainer container = new UnityContainer();
            container.RegisterType<ISearchFlightService, SearchFlightService>(new ContainerControlledLifetimeManager());
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
