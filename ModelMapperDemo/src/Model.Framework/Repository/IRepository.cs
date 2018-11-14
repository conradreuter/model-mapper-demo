using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Optional;

namespace ModelMapperDemo.Model.Framework.Repository
{
    /// <summary>
    /// Provides access to entities via a collection-like interface.
    /// </summary>
    public interface IRepository<TEntity>
        where TEntity : Entity<TEntity>
    {
        /// <summary>
        /// Create a new entity.
        /// </summary>
        TEntity Create();

        /// <summary>
        /// Fetch all entities.
        /// </summary>
        Task<IEnumerable<TEntity>> ListAsync(CancellationToken cancellationToken);
    }

    /// <summary>
    /// Type-unsafe base interface for repositories.
    /// </summary>
    /// <notes>
    /// This is for framework code only. Use the type-safe variants wherever possible.
    /// </notes>
    public interface IRepository
    {
        /// <summary>
        /// Create a new entity.
        /// </summary>
        /// <notes>
        /// This is for framework code only. Use the type-safe variants wherever possible.
        /// </notes>
        IEntity __UNSAFE__Create();

        /// <summary>
        /// Fetch all entities.
        /// </summary>
        /// <notes>
        /// This is for framework code only. Use the type-safe variants wherever possible.
        /// </notes>
        Task<IEnumerable<IEntity>> __UNSAFE__ListAsync(CancellationToken cancellationToken);
    }
}
