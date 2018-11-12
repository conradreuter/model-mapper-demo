namespace ModelMapperDemo.Model.Module
{
    partial class Parent
    {
        /// <summary>
        /// A name for uniquely identifying the parent entity.
        /// </summary>
        public static readonly Property<string> Name =
            new Value<string>(nameof(Name));
    }
}
