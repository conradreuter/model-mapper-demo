namespace ModelMapperDemo.Model.Framework
{
    partial class Entity<TEntity>
    {
        /// <summary>
        /// Defines a new value on an entity type in a builder-like fashion
        /// </summary>
        protected class Value<TValue> : Property<TValue>, IValue
        {
            public Value(string name) : base(name)
            {
            }
        }
    }
}
