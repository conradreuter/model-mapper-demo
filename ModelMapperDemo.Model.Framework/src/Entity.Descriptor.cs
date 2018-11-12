using System;
using System.Collections.Generic;
using System.Reflection;
using ModelMapperDemo.Utility.Extensions;

namespace ModelMapperDemo.Model.Framework
{
    partial class Entity<TEntity>
    {
        /// <summary>
        /// Describes the entity type.
        /// </summary>
        public static readonly IEntityDescriptor Descriptor = DescriptorBuilder.Instance;

        /// <summary>
        /// Defines an entity type in a builder-like fashion.
        /// </summary>
        protected class DescriptorBuilder : IEntityDescriptor
        {
            public static DescriptorBuilder Instance = new DescriptorBuilder();

            internal DescriptorBuilder()
            {
            }

            Type IEntityDescriptor.EntityType => typeof(TEntity);

            string IEntityDescriptor.Name => typeof(TEntity).Name;

            IEnumerable<IProperty> IEntityDescriptor.Properties => Entity<TEntity>.Properties;
        }
    }

    partial class Entity
    {
        /// <summary>
        /// Get the descriptor from an entity type instance.
        /// </summary>
        public static IEntityDescriptor GetDescriptorFromType(Type entityType)
        {
            entityType.RunStaticInitializer();
            return
                (IEntityDescriptor)
                entityType
                .BaseType
                .GetField("Descriptor", BindingFlags.Static | BindingFlags.Public)
                .GetValue(null);
        }
    }
}
