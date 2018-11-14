using System;
namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Describes a property.
    /// </summary>
    public interface IPropertyDescriptor
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
