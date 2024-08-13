using System.Linq;

namespace System.Collections.Generic;

public static class EnumerableExtensions
{
    public static bool SequenceIgnoredEqual<T>(this IEnumerable<T>? items1, IEnumerable<T>? items2)
    {
        if (Equals(items1, items2)) //两个相等（包括都是null）
            return true;

        if (items1 == null || items2 == null) //有一个为null，就是false
            return false;

        return items1.OrderBy(e => e).SequenceEqual(items2.OrderBy(e => e));
    }
}
