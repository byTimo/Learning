using System.Collections.Generic;

namespace Garden.Utility
{
    public static class EnumerableExtensions
    {
        public static void Enumerate<T>(this IEnumerable<T> enumerable)
        {
            foreach (var element in enumerable)
            {
            }
        }
    }
}