using System;
using System.Collections.Generic;

namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Describes an entity type
    /// </summary>
    public interface IEntityDescriptor
    {
        /// <summary>
        /// The underlying entity type.
        /// </summary>
        Type EntityType { get; }

        /// <summary>
        /// The name of the entity type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The properties defined on the entity type.
        /// </summary>
        IEnumerable<IProperty> Properties { get; }
    }
}
