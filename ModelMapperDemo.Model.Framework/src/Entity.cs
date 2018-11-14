using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Optional;

namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Base class for defining a new entity type.
    /// </summary>
    public abstract partial class Entity<TEntity> : IEntity
        where TEntity : Entity<TEntity>
    {
        private Option<Guid> _id;
        private readonly IDictionary<IPropertyDescriptor, object> _valuesByProperty =
            new Dictionary<IPropertyDescriptor, object>();

        /// <summary>
        /// The unique identifier (ID) of the entity.
        /// </summary>
        public Guid Id
        {
            get => _id.ValueOr(() => throw new InvalidOperationException("ID has not been set"));
            internal set => _id.Match
            (
                some: key => throw new InvalidOperationException("ID has already been set"),
                none: () => _id = Option.Some(value)
            );
        }

        /// <summary>
        /// Get the value of the property.
        /// </summary>
        public async Task<TValue> GetAsync<TValue>(
            Property<TValue> property,
            IPropertyAccessContext context)
        {
            var value = await ((IEntity)this).__UNSAFE__GetAsync(property, context);
            return (TValue)value;
        }

        /// <summary>
        /// Set the value of the property.
        /// </summary>
        public bool Set<TValue>(
            Property<TValue> property,
            TValue value,
            IPropertyAccessContext context) =>
            ((IEntity)this).__UNSAFE__Set(property, value, context);

        public override string ToString()
        {
            return $"{Descriptor.Name} {Id}";
        }

        Guid IEntity.Id => Id;

        Task<object> IEntity.__UNSAFE__GetAsync(
            IPropertyDescriptor property,
            IPropertyAccessContext context)
        {
            if (!_valuesByProperty.TryGetValue(property, out var value))
            {
                return Task.FromException<object>(new InvalidOperationException($"No value found for {property} in {this}"));
            }
            return Task.FromResult(value);
        }

        bool IEntity.__UNSAFE__Set(
            IPropertyDescriptor property,
            object value,
            IPropertyAccessContext context)
        {
            var hasChanged = true;
            if (_valuesByProperty.TryGetValue(property, out var oldValue))
            {
                // TODO: proper check
                hasChanged = !object.Equals(value, oldValue);
            }
            if (hasChanged)
            {
                _valuesByProperty[property] = value;
            }
            return hasChanged;
        }
    }

    public static partial class Entity
    {
    }
}
