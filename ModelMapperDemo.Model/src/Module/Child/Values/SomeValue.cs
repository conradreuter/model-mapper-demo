using ModelMapperDemo.Model.DomainTypes;

namespace ModelMapperDemo.Model.Module
{
    partial class Child
    {
        /// <summary>
        /// Some value that exists on the child entity per year and month.
        /// </summary>
        public static readonly Property<ByYearAndMonth<int>> SomeValue =
            new Value<ByYearAndMonth<int>>(nameof(SomeValue));
    }
}
