using Optional;

namespace ModelMapperDemo.Model.Module
{
    partial class Thing
    {
        /// <summary>
        /// A free-form text to describe details of the thing.
        /// </summary>
        public static readonly Property<Option<string>> Description =
            new Value<Option<string>>(nameof(Description));
    }
}
