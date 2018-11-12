using GraphQL.Types;

namespace ModelMapperDemo.GraphQL
{
    public static class GraphQLExtensions
    {
        /// <summary>
        /// Get the GraphQL execution context from a resolve context.
        /// </summary>
        public static GraphQLExecutionContext GetExecutionContext(this ResolveFieldContext resolveContext) =>
            (GraphQLExecutionContext)resolveContext.UserContext;

        /// <summary>
        /// Make the GraphQL type nullable if the condition is met.
        /// </summary>
        public static IGraphType MakeNullableIf(this IGraphType type, bool condition) =>
            condition ? new NonNullGraphType(type) : type;
    }
}
