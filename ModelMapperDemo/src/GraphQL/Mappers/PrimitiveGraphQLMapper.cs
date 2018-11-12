using System;
using System.Collections.Generic;
using GraphQL.Types;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Maps primitive values to GraphQL
    /// </summary>
    public class PrimitiveGraphQLMapper : ISwitchableGraphQLMapper
    {
        private static readonly IReadOnlyDictionary<Type, IGraphType> GraphTypesByType
            = new Dictionary<Type, IGraphType>
            {
                [typeof(int)] = new IntGraphType(),
                [typeof(decimal)] = new DecimalGraphType(),
                [typeof(string)] = new StringGraphType(),
                [typeof(bool)] = new BooleanGraphType(),
            };

        bool ISwitchableGraphQLMapper.CanMap(GraphQLMapperContext context) =>
            GraphTypesByType.ContainsKey(context.Type);

        IEnumerable<GraphQLFieldDefinition> IGraphQLMapper.CreateFields(GraphQLMapperContext context)
        {
            yield return
                GraphQLFieldDefinition
                .FromContext(context)
                .WithType(_ => GraphTypesByType[context.Type].MakeNullableIf(context.IsNullable));
        }
    }
}
