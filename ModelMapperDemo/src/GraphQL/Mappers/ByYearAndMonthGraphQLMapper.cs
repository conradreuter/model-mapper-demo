using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModelMapperDemo.GraphQL.DomainTypes;
using ModelMapperDemo.Model.DomainTypes;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Maps values by year and month to GraphQL
    /// </summary>
    public class ByYearAndMonthGraphQLMapper : ISwitchableGraphQLMapper
    {
        private readonly IGraphQLMapper _mapper;

        public ByYearAndMonthGraphQLMapper(IGraphQLMapper mapper)
        {
            _mapper = mapper;
        }

        bool ISwitchableGraphQLMapper.CanMap(GraphQLMapperContext context) =>
            context.Type.IsGenericType &&
            context.Type.GetGenericTypeDefinition() == typeof(ByYearAndMonth<>);

        IEnumerable<GraphQLFieldDefinition> IGraphQLMapper.CreateFields(GraphQLMapperContext context)
        {
            var innerContext = GetInnerContext(context);
            var unpackMethod =
                typeof(ByYearAndMonthGraphQLMapper)
                .GetMethod(nameof(Unpack), BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(innerContext.Type);

            return _mapper.CreateFields(innerContext).Select(field =>
                field
                .WithType(_ => new ByYearAndMonthGraphQLType().MakeNullableIf(context.IsNullable))
                .WithResolver(innerResolver => async resolveContext =>
                {
                    var value = await innerResolver(resolveContext);
                    return unpackMethod.Invoke(null, new[] { value });
                }));
        }

        private static IReadOnlyDictionary<int, IReadOnlyList<TInner>> Unpack<TInner>(
            ByYearAndMonth<TInner> value) =>
            value.Entries.ToDictionary(entry => entry.Year, entry => entry.Values);

        private static GraphQLMapperContext GetInnerContext(GraphQLMapperContext context) =>
            context
            .WithType(context.Type.GetGenericArguments()[0], false);
    }
}
