namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Maps a value to GraphQL if it meets certain criteria.
    /// </summary>
    public interface ISwitchableGraphQLMapper : IGraphQLMapper
    {
        /// <summary>
        /// Whether this mapper can map the property.
        /// </summary>
        bool CanMap(GraphQLMapperContext context);
    }
}
