namespace ModelMapperDemo.Model.Module
{
    partial class Thing
    {
        /// <summary>
        /// A name for uniquely identifying the thing.
        /// </summary>
        public static readonly Property<string> Name =
            new Value<string>(nameof(Name));
    }
}
