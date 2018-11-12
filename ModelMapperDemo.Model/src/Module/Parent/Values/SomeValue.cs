using ModelMapperDemo.Model.DomainTypes;

namespace ModelMapperDemo.Model.Module
{
    partial class Parent
    {
        /// <summary>
        /// Some value that exists on the parent entity per year and month.
        /// </summary>
        public static readonly Property<ByYearAndMonth<int>> SomeValue =
            new Value<ByYearAndMonth<int>>(nameof(SomeValue));
    }
}
