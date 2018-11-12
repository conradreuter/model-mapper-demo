using System;
using System.Collections.Generic;
using System.Linq;
using ModelMapperDemo.Utility.Extensions;

namespace ModelMapperDemo.Model.DomainTypes
{
    /// <summary>
    /// A value that is stored for each year and month.
    /// </summary>
    public sealed class ByYearAndMonth<TValue>
    {
        private readonly IReadOnlyDictionary<int, Entry> _entries;

        internal ByYearAndMonth(IEnumerable<Entry> entries)
        {
            _entries = entries.ToDictionary(entry => entry.Year);
        }

        /// <summary>
        /// The year entries, ordered by year.
        /// </summary>
        internal IEnumerable<Entry> Entries => _entries.Values.OrderBy(entry => entry.Year);

        /// <summary>
        /// Apply an aggregation to all values.
        /// </summary>
        public TResult Aggregate<TResult>(
            TResult seed,
            Func<TResult, TValue, int, int, TResult> aggregation) =>
            _entries.Values.Aggregate(seed, (acc, entry) => entry.Aggregate(acc, aggregation));

        /// <summary>
        /// Apply a projection to all values.
        /// </summary>
        public ByYearAndMonth<TResult> Select<TResult>(Func<TValue, int, int, TResult> projection)
        {
            var entries = _entries.Values.Select(entry => entry.Select(projection));
            return new ByYearAndMonth<TResult>(entries);
        }

        /// <summary>
        /// An entry representing one year in a ByYearAndMonth.
        /// </summary>
        internal class Entry
        {
            internal Entry(int year, IEnumerable<TValue> values)
            {
                Year = year;
                Values = values.ToList();
                if (Values.Count != 12)
                {
                    throw new ArgumentException($"Wrong number of values, expected 12, got {Values.Count}", nameof(values));
                }
            }

            /// <summary>
            /// The values stored for the year.
            /// </summary>
            public IReadOnlyList<TValue> Values { get; }

            /// <summary>
            /// The year this entry applies to.
            /// </summary>
            public int Year { get; }

            /// <summary>
            /// Apply an aggregation to all values.
            /// </summary>
            public TResult Aggregate<TResult>(
                TResult seed,
                Func<TResult, TValue, int, int, TResult> aggregation) =>
                Values
                .Indexed()
                .Aggregate(seed, (acc, next) => aggregation(acc, next.element, Year, next.index + 1));

            /// <summary>
            /// Apply a projection to all values.
            /// </summary>
            public ByYearAndMonth<TResult>.Entry Select<TResult>(
                Func<TValue, int, int, TResult> projection)
            {
                var values = Values.Select((value, index) => projection(value, Year, index + 1));
                return new ByYearAndMonth<TResult>.Entry(Year, values);
            }
        }
    }
}
