using System;
using System.Collections.Generic;
using GraphQL.Types;
using ModelMapperDemo.Model.Framework;

namespace ModelMapperDemo.GraphQL.Types
{
    /// <summary>
    /// The GraphQL type for the root query object.
    /// </summary>
    public class QueryGraphQLType : ObjectGraphType
    {
        public QueryGraphQLType(
            IEnumerable<IEntityDescriptor> entityDescriptors,
            Func<IEntityDescriptor, GetEntityGraphQLType> getEntityFactory,
            Func<IEntityDescriptor, ListEntitiesGraphQLType> listEntitiesFactory)
        {
            Name = "Query";
            Description = "The root query type.";

            foreach (var descriptor in entityDescriptors)
            {
                AddField(getEntityFactory(descriptor));
                AddField(listEntitiesFactory(descriptor));
            }
        }
    }
}
