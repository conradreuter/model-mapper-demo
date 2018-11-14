using System;
using ModelMapperDemo.Model.Framework;

namespace ModelMapperDemo.GraphQL.Mappers
{
    /// <summary>
    /// Provides context for mapping values to GraphQL.
    /// </summary>
    public class GraphQLMapperContext
    {
        private GraphQLMapperContext()
        {
        }

        /// <summary>
        /// Create a new context from the property.
        /// </summary>
        public static GraphQLMapperContext FromProperty(IPropertyDescriptor property) =>
            new GraphQLMapperContext
            {
                IsNullable = false,
                Property = property,
                Type = property.Type,
            };

        /// <summary>
        /// Whether the field is nullable.
        /// </summary>
        public bool IsNullable { get; private set; }

        /// <summary>
        /// The original property to be mapped.
        /// </summary>
        public IPropertyDescriptor Property { get; private set; }

        /// <summary>
        /// The type of value to be mapped.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Create a new context with a different type and - if defined - a different nullability.
        /// </summary>
        public GraphQLMapperContext WithType(Type type, bool? isNullable = null) =>
            Copy(copy =>
            {
                copy.Type = type;
                if (isNullable.HasValue) copy.IsNullable = isNullable.Value;
            });

        private GraphQLMapperContext Copy(Action<GraphQLMapperContext> modify)
        {
            var copy = (GraphQLMapperContext)MemberwiseClone();
            modify(copy);
            return copy;
        }
    }
}
