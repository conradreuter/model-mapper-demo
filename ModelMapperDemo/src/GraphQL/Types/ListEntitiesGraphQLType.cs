using System.Collections.Generic;
using Autofac.Features.Indexed;
using GraphQL.Resolvers;
using GraphQL.Types;
using Humanizer;
using ModelMapperDemo.Model.Framework;
using ModelMapperDemo.Model.Framework.Repository;

namespace ModelMapperDemo.GraphQL.Types
{
    /// <summary>
    /// GraphQL query for a all entities.
    /// </summary>
    public class ListEntitiesGraphQLType : FieldType
    {
        public ListEntitiesGraphQLType(
            IEntityDescriptor descriptor,
            IIndex<IEntityDescriptor, EntityGraphQLType> entityTypesByDescriptor,
            IIndex<IEntityDescriptor, IRepository> repositoriesByDescriptor)
        {
            Name = descriptor.Name.Pluralize().Camelize();
            Description = $"Query all {descriptor.Name.Pluralize().Humanize(LetterCasing.LowerCase)}";
            ResolvedType = new ListGraphType(new NonNullGraphType(entityTypesByDescriptor[descriptor]));

            var repository = repositoriesByDescriptor[descriptor];
            Resolver = new AsyncFieldResolver<IEnumerable<IEntity>>(resolveContext =>
            {
                return repository.__UNSAFE__ListAsync(resolveContext.CancellationToken);
            });
        }
    }
}
