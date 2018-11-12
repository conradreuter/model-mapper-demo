using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ModelMapperDemo.Model.DomainTypes;
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
            builder.RegisterBuildCallback(container =>
            {
                var factory = new GenericFactory(container);
                var context = new PropertyAccessContext();
                SeedAsync(factory, context).Wait();
            });
        }

        private async Task SeedAsync(GenericFactory factory, IPropertyAccessContext context)
        {
            var parent = await factory.CreateAsync<Parent>();
            await parent.SetAsync(Parent.Name, "Parent", context);
            await parent.SetAsync(Parent.Description, "Description of Parent", context);
            await parent.SetAsync(Parent.SomeValue, new ByYearAndMonth<int>(new[] {
                new ByYearAndMonth<int>.Entry(2018, new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }),
                new ByYearAndMonth<int>.Entry(2019, new[] { 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24 }),
            }), context);

            var anotherParent = await factory.CreateAsync<Parent>();
            await anotherParent.SetAsync(Parent.Name, "Another Parent", context);
            await anotherParent.SetAsync(Parent.Description, "Description of Another Parent", context);
            await anotherParent.SetAsync(Parent.SomeValue, ByYearAndMonth<int>.Empty, context);
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
