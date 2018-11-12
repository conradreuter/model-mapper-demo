using System;
using System.Reflection;
using Autofac;
using ModelMapperDemo.Model.Framework.Repository;

namespace ModelMapperDemo.Model.Framework
{
    public class ModelframeworkWireup : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            foreach (var descriptor in ModelWireup.AllEntityDescriptors)
            {
                typeof(ModelframeworkWireup)
                    .GetMethod(nameof(RegisterEntityType), BindingFlags.Instance | BindingFlags.NonPublic)
                    .MakeGenericMethod(descriptor.EntityType)
                    .Invoke(this, new object[] { descriptor, builder });
            }
        }

        private void RegisterEntityType<TEntity>(IEntityDescriptor descriptor, ContainerBuilder builder)
            where TEntity : Entity<TEntity>
        {
            builder
                .RegisterType<TransientRepository>()
                .Keyed<IRepository>(descriptor)
                .SingleInstance();

            builder
                .Register(ctx =>
                {
                    var repository = ctx.ResolveKeyed<IRepository>(descriptor);
                    return new TypesafeRepository<TEntity>(repository);
                })
                .As<IRepository<TEntity>>()
                .SingleInstance();
        }
    }
}
