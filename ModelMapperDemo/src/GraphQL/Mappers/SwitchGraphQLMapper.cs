using System;
using System.Collections.Generic;
using Autofac.Features.Indexed;
using Optional.Collections;
using ModelMapperDemo.GraphQL.Types;
using ModelMapperDemo.Model.Framework;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Maps properties to GraphQL by selecting an appropriate mapper based on the value type.
    /// </summary>
    public class SwitchGraphQLMapper : IGraphQLMapper
    {
        private readonly IReadOnlyCollection<ISwitchableGraphQLMapper> _mappers;

        public SwitchGraphQLMapper(
            IIndex<IEntityDescriptor, EntityGraphQLType> entityTypesByDescriptor)
        {
            _mappers = new List<ISwitchableGraphQLMapper>
            {
                new OptionGraphQLMapper(this),
                new ByYearAndMonthGraphQLMapper(this),
                new EntityGraphQLMapper(entityTypesByDescriptor),
                new PrimitiveGraphQLMapper(),
            };
        }

        IEnumerable<GraphQLFieldDefinition> IGraphQLMapper.CreateFields(GraphQLMapperContext context) =>
            SelectMapper(context).CreateFields(context);

        private IGraphQLMapper SelectMapper(GraphQLMapperContext context) =>
            _mappers
            .FirstOrNone(m => m.CanMap(context))
            .ValueOr(() => throw new InvalidOperationException($"No appropriate GraphQL mapper found for {context.Property}"));
    }
}
