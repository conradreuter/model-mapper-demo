namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// Type-unsafe base interface for relation properties.
    /// </summary>
    public interface IRelation : IProperty
    {
        /// <summary>
        /// The kind of relation.
        /// </summary>
        RelationKind Kind { get; }

        /// <summary>
        /// The entity type this relation points to.
        /// </summary>
        IEntityDescriptor RelatedEntity { get; }
    }
}
