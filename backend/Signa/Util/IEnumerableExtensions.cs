using System.Collections.Generic;
using System.Linq;

namespace Signa.Util
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TInput> Concatenate<TInput>(this IEnumerable<IEnumerable<TInput>> enumerables)
        {
            IEnumerable<TInput> collection = new TInput[0];

            return enumerables.Aggregate(collection, (current, enumerable) => current.Concat(enumerable));
        }
    }
}