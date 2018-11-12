using System;
using System.Collections.Generic;

namespace ModelMapperDemo.Model.Framework
{
    partial class Entity<TEntity>
    {
        private static readonly ISet<IProperty> Properties = new HashSet<IProperty>();

        /// <summary>
        /// Defines a new property on an entity type.
        /// </summary>
        public abstract class Property<TValue> : IProperty
        {
            private readonly string _name;

            protected Property(string name)
            {
                Properties.Add(this);
                _name = name;
            }

            public override string ToString()
            {
                return $"{Descriptor.Name}.{_name}";
            }

            IEntityDescriptor IProperty.Entity => Descriptor;

            string IProperty.Name => _name;

            Type IProperty.Type => typeof(TValue);
        }
    }
}
