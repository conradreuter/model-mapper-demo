using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Optional;
using Optional.Unsafe;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Maps optional values to GraphQL
    /// </summary>
    public class OptionGraphQLMapper : ISwitchableGraphQLMapper
    {
        private readonly IGraphQLMapper _mapper;

        public OptionGraphQLMapper(IGraphQLMapper mapper)
        {
            _mapper = mapper;
        }

        bool ISwitchableGraphQLMapper.CanMap(GraphQLMapperContext context) =>
            context.Type.IsGenericType &&
            context.Type.GetGenericTypeDefinition() == typeof(Option<>);

        IEnumerable<GraphQLFieldDefinition> IGraphQLMapper.CreateFields(GraphQLMapperContext context)
        {
            var innerContext = GetInnerContext(context);
            var unpackMethodName =
                innerContext.Type.IsValueType
                    ? nameof(UnpackValue)
                    : nameof(UnpackReference);
            var unpackMethod =
                typeof(OptionGraphQLMapper)
                .GetMethod(unpackMethodName, BindingFlags.Static | BindingFlags.NonPublic)
                .MakeGenericMethod(innerContext.Type);
            return _mapper.CreateFields(innerContext).Select(field =>
                field
                .WithResolver(innerResolver => async resolveContext =>
                {
                    var valueOption = await innerResolver(resolveContext);
                    return unpackMethod.Invoke(null, new[] { valueOption });
                }));
        }

        private static TValue UnpackReference<TValue>(Option<TValue> valueOption)
            where TValue : class =>
            valueOption.ValueOrDefault();

        private static TValue? UnpackValue<TValue>(Option<TValue> valueOption)
            where TValue : struct =>
            valueOption.ToNullable();

        private static GraphQLMapperContext GetInnerContext(GraphQLMapperContext context) =>
            context
            .WithType(context.Type.GetGenericArguments()[0], true);
    }
}
