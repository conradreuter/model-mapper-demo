namespace ModelMapperDemo.Model.Module
{
    partial class Parent
    {
        /// <summary>
        /// A free-form text to describe details of the parent entity.
        /// </summary>
        public static readonly Property<string> Description =
            new Value<string>(nameof(Description));
    }
}
