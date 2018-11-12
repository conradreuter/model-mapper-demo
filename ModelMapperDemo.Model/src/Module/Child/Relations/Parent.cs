namespace ModelMapperDemo.Model.Module
{
    partial class Child
    {
        /// <summary>
        /// The parent entity this child entity belongs to.
        /// </summary>
        public static readonly Property<Parent> Parent =
            new Relation<Parent>(nameof(Parent));
    }
}
