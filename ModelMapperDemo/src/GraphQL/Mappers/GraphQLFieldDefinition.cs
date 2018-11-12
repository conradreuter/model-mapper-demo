using System;
using System.Threading.Tasks;
using GraphQL.Types;
using Humanizer;
using ModelMapperDemo.Model.Framework;
using ModelMapperDemo.Utility;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Defines a field of an entity GraphQL type.
    /// </summary>
    public class GraphQLFieldDefinition
    {
        private IGraphType _type;

        private GraphQLFieldDefinition()
        {
        }

        /// <summary>
        /// Create a GraphQL field resolver for a GraphQL mapper context.
        /// </summary>
        public static GraphQLFieldDefinition FromContext(GraphQLMapperContext context) =>
            new GraphQLFieldDefinition
            {
                Name = context.Property.Name.Camelize(),
                Description = XmlDoc.ReadSummary(context.Property.Entity.EntityType.GetField(context.Property.Name)),
                Resolver = resolveContext =>
                {
                    var entity = (IEntity)resolveContext.Source;
                    var executionContext = resolveContext.GetExecutionContext();
                    return entity.__UNSAFE__GetAsync(context.Property, executionContext.PropertyAccess);
                },
            };

        /// <summary>
        /// The description of the GraphQL field.
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The name of the GraphQL field.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// How the value of the field is resolved.
        /// </summary>
        public Func<ResolveFieldContext, Task<object>> Resolver { get; private set; }

        /// <summary>
        /// The GraphQL type of the field.
        /// </summary>
        public IGraphType Type => _type ?? throw new InvalidOperationException("Type not yet specified");

        /// <summary>
        /// Create a new field definiton with a different name.
        /// </summary>
        public GraphQLFieldDefinition WithName(Func<string, string> makeName) =>
            Copy(copy => copy.Name = makeName(Name));

        /// <summary>
        /// Create a new field definiton with a different description.
        /// </summary>
        public GraphQLFieldDefinition WithDescription(Func<string, string> makeDescription) =>
            Copy(copy => copy.Description = makeDescription(Description));

        /// <summary>
        /// Create a new field definiton with a different resolver.
        /// </summary>
        public GraphQLFieldDefinition WithResolver(Func<Func<ResolveFieldContext, Task<object>>, Func<ResolveFieldContext, Task<object>>> makeResolver) =>
            Copy(copy => copy.Resolver = makeResolver(Resolver));

        /// <summary>
        /// Create a new field definiton with a different type.
        /// </summary>
        public GraphQLFieldDefinition WithType(Func<IGraphType, IGraphType> makeType) =>
            Copy(copy => copy._type = makeType(_type));

        private GraphQLFieldDefinition Copy(Action<GraphQLFieldDefinition> modify)
        {
            var copy = (GraphQLFieldDefinition)MemberwiseClone();
            modify(copy);
            return copy;
        }
    }
}
