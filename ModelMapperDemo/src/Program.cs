using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ModelMapperDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //*
            BuildWebHost(args).Run();
            /*/
            Test.Create().Run();
            //*/
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services.AddAutofac())
                .UseStartup<Startup>()
                .Build();
    }
}
