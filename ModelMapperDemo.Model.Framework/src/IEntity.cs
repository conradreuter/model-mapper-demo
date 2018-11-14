using System;
using System.Threading.Tasks;

namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Base interface for entities.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Get the value of a property.
        /// </summary>
        /// <notes>
        /// This is for framework code only. Use the type-safe variants wherever possible.
        /// </notes>
        Task<object> __UNSAFE__GetAsync(
            IPropertyDescriptor property,
            IPropertyAccessContext context);

        /// <summary>
        /// Set the value of a property.
        /// </summary>
        /// <notes>
        /// This is for framework code only. Use the type-safe variants wherever possible.
        /// </notes>
        bool __UNSAFE__Set(
            IPropertyDescriptor property,
            object value,
            IPropertyAccessContext context);
    }
}
