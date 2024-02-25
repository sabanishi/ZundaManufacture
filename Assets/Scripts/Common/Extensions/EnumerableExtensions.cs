using System.Collections.Generic;
using System.Linq;

namespace Sabanishi.ZundaManufacture
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source switch
            {
                null => true,
                ICollection<T> collection => collection.Count == 0,
                IReadOnlyCollection<T> readOnlyCollection => readOnlyCollection.Count == 0,
                _ => !source.Any()
            };
        }

        public static bool IsOutOfRange<T>(this IList<T> source, int index)
        {
            if (source == null) return true;
            return index < 0 || index >= source.Count;
        }

        public static bool TryGet<T>(this IList<T> source, int index, out T element)
        {
            element = default;
            if (source.IsNullOrEmpty()) return false;
            if (source.IsOutOfRange(index)) return false;
            element = source[index];
            return true;
        }
    }
}