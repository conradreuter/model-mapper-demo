using ModelMapperDemo.Model.Framework;

namespace ModelMapperDemo.GraphQL
{
    /// <summary>
    /// The context passed around by GraphQL queries and mutations.
    /// </summary>
    public class GraphQLExecutionContext
    {
        /// <summary>
        /// The property access context for the GraphQL request.
        /// </summary>
        public PropertyAccessContext PropertyAccess { get; } = new PropertyAccessContext();
    }
}
