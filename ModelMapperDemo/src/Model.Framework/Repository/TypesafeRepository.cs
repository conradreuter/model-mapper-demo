using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Optional;

namespace ModelMapperDemo.Model.Framework.Repository
{
    /// <summary>
    /// Provides a type-safe repository implementation on top of an unsafe repository.
    /// </summary>
    public class TypesafeRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity<TEntity>
    {
        private readonly IRepository _repository;

        public TypesafeRepository(
            IRepository repository)
        {
            _repository = repository;
        }

        TEntity IRepository<TEntity>.Create() => (TEntity)_repository.__UNSAFE__Create();

        Task<IEnumerable<TEntity>> IRepository<TEntity>.ListAsync(CancellationToken cancellationToken) =>
            _repository.__UNSAFE__ListAsync(cancellationToken)
                .ContinueWith(t => t.Result.Cast<TEntity>(), cancellationToken);
    }
}
