using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using ModelMapperDemo.Model.Framework;

namespace ModelMapperDemo.Model
{
    public class ModelWireup : Autofac.Module
    {
        /// <summary>
        /// The descriptors of all defined entities.
        /// </summary>
        public static readonly IEnumerable<IEntityDescriptor> AllEntityDescriptors =
            Assembly.Load("ModelMapperDemo.Model")
            .GetTypes()
            .Where(typeof(IEntity).IsAssignableFrom)
            .Select(Entity.GetDescriptorFromType)
            .ToArray();

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var descriptor in AllEntityDescriptors)
            {
                builder.RegisterInstance(descriptor);
                builder.RegisterType(descriptor.EntityType);
            }
        }
    }
}
