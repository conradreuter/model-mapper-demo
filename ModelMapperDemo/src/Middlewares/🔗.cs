using Autofac;
using Microsoft.AspNetCore.Http;

namespace ModelMapperDemo.Middlewares
{
    public class MiddlewaresWireup : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .InNamespace("ModelMapperDemo.Middlewares")
                .AssignableTo<IMiddleware>()
                .AsSelf();
        }
    }
}
