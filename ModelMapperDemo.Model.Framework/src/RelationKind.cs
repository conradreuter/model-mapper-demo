namespace ModelMapperDemo.Model.Framework
{
    /// <summary>
    /// The different kinds of relations between entities.
    /// </summary>
    public enum RelationKind
    {
        /// <summary>
        /// A dependent relation from one element to another.
        /// </summary>
        Dependent,

        /// <summary>
        /// An optional relation from one element to another.
        /// </summary>
        Optional,

        /// <summary>
        /// A relation from one element to multiple other elements.
        /// </summary>
        Multiple,
    }
}
