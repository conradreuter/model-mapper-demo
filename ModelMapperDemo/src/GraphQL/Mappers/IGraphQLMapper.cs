using System.Collections.Generic;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Maps a property to GraphQL.
    /// </summary>
    public interface IGraphQLMapper
    {
        /// <summary>
        /// Create the GraphQL fields for a property.
        /// </summary>
        IEnumerable<GraphQLFieldDefinition> CreateFields(GraphQLMapperContext context);
    }
}
