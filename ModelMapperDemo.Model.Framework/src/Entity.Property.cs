using System;
using System.Collections.Generic;

namespace ModelMapperDemo.Model.Framework
{
    partial class Entity<TEntity>
    {
        private static readonly ISet<IPropertyDescriptor> Properties =
            new HashSet<IPropertyDescriptor>();

        /// <summary>
        /// Defines a new property on an entity type.
        /// </summary>
        public abstract class Property<TValue> : IPropertyDescriptor
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

            IEntityDescriptor IPropertyDescriptor.Entity => Descriptor;

            string IPropertyDescriptor.Name => _name;

            Type IPropertyDescriptor.Type => typeof(TValue);
        }
    }
}
