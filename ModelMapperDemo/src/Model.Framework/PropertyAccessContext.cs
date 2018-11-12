using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Provides context for accessing properties.
    /// </summary>
    public class PropertyAccessContext : IPropertyAccessContext
    {
        /// <summary>
        /// The cancellation token for the whole property access operation.
        /// Usually tied to the request or operation.
        /// </summary>
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;
    }
}
