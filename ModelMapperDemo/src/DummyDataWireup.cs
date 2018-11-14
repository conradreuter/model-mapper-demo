using System.Collections.Generic;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ModelMapperDemo.Model.Framework;
using ModelMapperDemo.Model.Framework.Repository;
using ModelMapperDemo.Model.Module;

namespace ModelMapperDemo
{
    // TODO: remove this
    public class DummyDataWireup : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container => Seed(container));
        }

        private void Seed(ILifetimeScope container)
        {
            var context = new PropertyAccessContext();
            var thingRepository = container.Resolve<IRepository<TEntity>>();

            var thing = thingRepository.Create();
            thing.Set(Thing.Name, "Thing", context);
            //await thing.Set(Thing.Description, "Description of Thing", context);

            var anotherThing = thingRepository.Create();
            anotherThing.Set(Thing.Name, "Another Thing", context);
            //await anotherThing.Set(Thing.Description, "Description of Another Thing", context);
        }

        private class GenericFactory
        {
            private readonly IContainer _container;

            public GenericFactory(IContainer container)
            {
                _container = container;
            }

            public async Task<TEntity> CreateAsync<TEntity>()
                where TEntity : Entity<TEntity>, new()
            {
                var entity = new TEntity();
                entity.Id = Guid.NewGuid();

                var repository = _container.Resolve<IRepository<TEntity>>();
                await repository.AddAsync(entity, CancellationToken.None);

                return entity;
            }
        }
    }
}
