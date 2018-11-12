using System;
using System.Threading;
using System.Threading.Tasks;

namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Provides context for accessing properties.
    /// </summary>
    public interface IPropertyAccessContext
    {
        /// <summary>
        /// The cancellation token for the whole property access operation.
        /// </summary>
        CancellationToken CancellationToken { get; }
    }
}
