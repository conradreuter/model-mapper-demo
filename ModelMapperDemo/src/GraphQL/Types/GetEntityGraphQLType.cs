using System;
using Autofac.Features.Indexed;
using GraphQL.Resolvers;
using GraphQL.Types;
using Humanizer;
using Optional;
using Optional.Collections;
using ModelMapperDemo.Model.Framework;
using ModelMapperDemo.Model.Framework.Repository;

namespace ModelMapperDemo.GraphQL.Types
{
    /// <summary>
    /// GraphQL query for a single entity.
    /// </summary>
    public class GetEntityGraphQLType : FieldType
    {
        public GetEntityGraphQLType(
            IEntityDescriptor descriptor,
            IIndex<IEntityDescriptor, EntityGraphQLType> entityTypesByDescriptor,
            IIndex<IEntityDescriptor, IRepository> repositoriesByDescriptor)
        {
            Name = descriptor.Name.Camelize();
            Description = $"Query a single {descriptor.Name.Humanize(LetterCasing.LowerCase)}";
            ResolvedType = entityTypesByDescriptor[descriptor];
            Arguments = new QueryArguments
            {
                new QueryArgument(new NonNullGraphType(new IdGraphType()))
                {
                    Name = "id",
                    Description = $"The ID of the {descriptor.Name.Humanize(LetterCasing.LowerCase)}",
                },
            };

            var repository = repositoriesByDescriptor[descriptor];
            Resolver = new AsyncFieldResolver<Option<IEntity>>(async resolveContext =>
            {
                // TODO: optimize
                var id = resolveContext.GetArgument<Guid>("id");
                var entities = await repository.__UNSAFE__ListAsync(resolveContext.CancellationToken);
                return entities.FirstOrNone(e => e.Id == id);
            });
        }
    }
}
