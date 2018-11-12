using System.Collections.Generic;
using Autofac.Features.Indexed;
using GraphQL.Types;
using ModelMapperDemo.GraphQL.Types;
using ModelMapperDemo.Model.Framework;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Maps entities to GraphQL.
    /// </summary>
    public class EntityGraphQLMapper : ISwitchableGraphQLMapper
    {
        private readonly IIndex<IEntityDescriptor, EntityGraphQLType> _entityTypesByDescriptor;

        public EntityGraphQLMapper(
            IIndex<IEntityDescriptor, EntityGraphQLType> entityTypesByDescriptor)
        {
            _entityTypesByDescriptor = entityTypesByDescriptor;
        }

        bool ISwitchableGraphQLMapper.CanMap(GraphQLMapperContext context) =>
            typeof(IEntity).IsAssignableFrom(context.Type);

        IEnumerable<GraphQLFieldDefinition> IGraphQLMapper.CreateFields(GraphQLMapperContext context)
        {
            var descriptor = Entity.GetDescriptorFromType(context.Type);
            var entityType = _entityTypesByDescriptor[descriptor];
            yield return
                GraphQLFieldDefinition
                .FromContext(context)
                .WithType(_ => entityType.MakeNullableIf(context.IsNullable));

            yield return
                GraphQLFieldDefinition
                .FromContext(context)
                .WithName(name => $"{name}Id")
                .WithDescription(description => $"{description} (ID)")
                .WithType(_ => new IdGraphType().MakeNullableIf(context.IsNullable))
                .WithResolver(resolver => async resolveContext =>
                {
                    var entity = (IEntity)await resolver(resolveContext);
                    return entity.Id;
                });
        }
    }
}
