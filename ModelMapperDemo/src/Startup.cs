using Autofac;
using GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ModelMapperDemo.Middlewares;

namespace ModelMapperDemo
{
    public class Startup
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterAssemblyModules(typeof(Startup).Assembly);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseGraphiQl("/graphql");
            app.Map("/graphql", branch => branch.UseMiddleware<GraphQLMiddleware>());
        }
    }
}
