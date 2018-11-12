using System;
namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Type-unsafe base interface for properties.
    /// </summary>
    public interface IProperty
    {
        /// <summary>
        /// The descriptor of the type of entity this property belongs to.
        /// </summary>
        IEntityDescriptor Entity { get; }

        /// <summary>
        /// The name of this property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The type of value stored in this property.
        /// </summary>
        Type Type { get; }
    }
}
