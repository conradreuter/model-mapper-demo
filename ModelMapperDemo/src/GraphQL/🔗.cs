using Autofac;
using GraphQL.Types;
using ModelMapperDemo.GraphQL.Mappers;
using ModelMapperDemo.GraphQL.Types;
using ModelMapperDemo.Model;

namespace ModelMapperDemo.GraphQL
{
    public class GraphQLWireup : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<GraphQLSchema>()
                .As<ISchema>()
                .SingleInstance();

            builder.RegisterType<GraphQLExecutionContext>();

            builder
                .RegisterType<SwitchGraphQLMapper>()
                .As<IGraphQLMapper>()
                .SingleInstance();

            builder
                .RegisterAssemblyTypes(ThisAssembly)
                .InNamespace("ModelMapperDemo.GraphQL.Types")
                .Except<EntityGraphQLType>()
                .AsSelf();

            foreach (var descriptor in ModelWireup.AllEntityDescriptors)
            {
                builder
                    .RegisterType<EntityGraphQLType>()
                    .WithParameter(TypedParameter.From(descriptor))
                    .Keyed(descriptor, typeof(EntityGraphQLType))
                    .SingleInstance()
                    .OnActivated(args => args.Instance.AddAllPropertyFields());
            }
        }
    }
}
