using GraphQL.Types;
using ModelMapperDemo.GraphQL.Types;

namespace ModelMapperDemo.GraphQL
{
    /// <summary>
    /// The GraphQL schema used for the application.
    /// </summary>
    public class GraphQLSchema : Schema
    {
        public GraphQLSchema(
            QueryGraphQLType query)
        {
            Query = query;
        }
    }
}
