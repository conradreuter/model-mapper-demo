using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Optional;
using Optional.Collections;

namespace ModelMapperDemo.Model.Framework.Repository
{
    /// <summary>
    /// Stores entities in-memory.
    /// </summary>
    public sealed class TransientRepository : IRepository
    {
        private readonly ISet<IEntity> _entities =
            new HashSet<IEntity>();

        IEntity IRepository.__UNSAFE__Create()
        {
            _entities.Add(entity);
            return Task.CompletedTask;
        }

        Task<IEnumerable<IEntity>> IRepository.__UNSAFE__ListAsync(CancellationToken cancellationToken) =>
            Task.FromResult(_entities.AsEnumerable());
    }
}
