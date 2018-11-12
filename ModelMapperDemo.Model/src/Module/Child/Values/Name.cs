namespace ModelMapperDemo.Model.Module
{
    partial class Child
    {
        /// <summary>
        /// Identifies the child entity amongst others within the same parent entity.
        /// </summary>
        public static readonly Property<string> Name =
            new Value<string>(nameof(Name));
    }
}
