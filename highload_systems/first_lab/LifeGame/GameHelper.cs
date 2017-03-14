using System.Collections.Generic;

namespace LifeGame
{
    public static class GameHelper
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }
    }
}