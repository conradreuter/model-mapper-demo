using System.Collections.Generic;
using System.Linq;

namespace ModelMapperDemo.Utility.Extensions
{
    public static class Enumerables
    {
        /// <summary>
        /// Create a new sequence by attaching the sequence index to each value of a sequence.
        /// </summary>
        public static IEnumerable<(TElement element, int index)> Indexed<TElement>(this IEnumerable<TElement> sequence) =>
            sequence.Select((element, index) => (element, index));

        /// <summary>
        /// Create a sequence of elements.
        /// </summary>
        public static IEnumerable<TElement> Of<TElement>(params TElement[] elements) => elements;
    }
}
