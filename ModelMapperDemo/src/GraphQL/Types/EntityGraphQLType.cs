using System;
using System.Collections.Generic;
using GraphQL.Resolvers;
using GraphQL.Types;
using Humanizer;
using ModelMapperDemo.GraphQL.Mappers;
using ModelMapperDemo.Model.Framework;
using ModelMapperDemo.Utility;

namespace ModelMapperDemo.GraphQL.Types
{
    /// <summary>
    /// The GraphQL representation of an entity type.
    /// </summary>
    public class EntityGraphQLType : ObjectGraphType
    {
        private readonly IGraphQLMapper _mapper;
        private readonly IEnumerable<IProperty> _properties;

        public EntityGraphQLType(
            IEntityDescriptor descriptor,
            IGraphQLMapper mapper)
        {
            _mapper = mapper;
            _properties = descriptor.Properties;

            Name = descriptor.Name;
            Description = XmlDoc.ReadSummary(descriptor.EntityType);

            AddField(new FieldType
            {
                Name = "id",
                Description = $"The ID of the {descriptor.Name.Humanize(LetterCasing.LowerCase)}.",
                ResolvedType = new NonNullGraphType(new IdGraphType()),
                Resolver = new FuncFieldResolver<IEntity, Guid>(resolveContext => resolveContext.Source.Id),
            });
        }

        /// <summary>
        /// Add the fields for the entity's properties to this GraphQL type.
        /// </summary>
        /// <notes>
        /// This cannot be done in the constructor, because the GraphQL types may cyclically
        /// depend on each other.
        /// </notes>
        public void AddAllPropertyFields()
        {
            foreach (var property in _properties)
            {
                var context = GraphQLMapperContext.FromProperty(property);
                foreach (var field in _mapper.CreateFields(context))
                {
                    AddField(new FieldType
                    {
                        Name = field.Name,
                        Description = field.Description,
                        ResolvedType = field.Type,
                        Resolver = new FuncFieldResolver<object>(field.Resolver),
                    });
                }
            }
        }
    }
}
