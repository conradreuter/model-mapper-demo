using System;
using System.Collections.Generic;
using Optional;

namespace ModelMapperDemo.Model.Framework
{
    partial class Entity<TEntity>
    {
        /// <summary>
        /// Defines a new relation on an entity type in a builder-like fashion
        /// </summary>
        protected class Relation<TValue> : Property<TValue>, IRelation
        {
            private readonly RelationKind _kind;
            private readonly IEntityDescriptor _relatedEntity;

            public Relation(string name) : base(name)
            {
                var (kind, relatedEntityType) =
                    DetectDependentRelation() ??
                    DetectOptionalRelation() ??
                    DetectMultipleRelation() ??
                    throw new InvalidOperationException($"Unable to determine relation kind for {this}");
                _kind = kind;
                _relatedEntity = Entity.GetDescriptorFromType(relatedEntityType);
            }

            private static (RelationKind kind, Type relatedEntityType)? DetectDependentRelation()
            {
                var isEntityType = typeof(IEntity).IsAssignableFrom(typeof(TValue));
                if (!isEntityType) return null;
                return (RelationKind.Dependent, typeof(TValue));
            }

            private static (RelationKind kind, Type relatedEntityType)? DetectOptionalRelation()
            {
                var isOptionType =
                    typeof(TValue).IsGenericType &&
                    typeof(TValue).GetGenericTypeDefinition() == typeof(Option<>);
                if (!isOptionType) return null;
                var innerType = typeof(TValue).GetGenericArguments()[0];
                var isInnerEntityType = typeof(IEntity).IsAssignableFrom(innerType);
                if (!isInnerEntityType) return null;
                return (RelationKind.Optional, innerType);
            }

            private static (RelationKind kind, Type relatedEntityType)? DetectMultipleRelation()
            {
                var isEnumerableType =
                    typeof(TValue).IsGenericType &&
                    typeof(TValue).GetGenericTypeDefinition() == typeof(IEnumerable<>);
                if (!isEnumerableType) return null;
                var innerType = typeof(TValue).GetGenericArguments()[0];
                var isInnerEntityType = typeof(IEntity).IsAssignableFrom(innerType);
                if (!isInnerEntityType) return null;
                return (RelationKind.Multiple, innerType);
            }

            RelationKind IRelation.Kind => _kind;

            IEntityDescriptor IRelation.RelatedEntity => _relatedEntity;

            /// <summary>
            /// Makes this property the inverse of another property.
            /// </summary>
            public Relation<TValue> InverseTo(IProperty property)
            {
                if (!(property is IRelation relation))
                {
                    throw new InvalidOperationException($"{property} is not a relation");
                }
                return this;
            }
        }
    }
}
