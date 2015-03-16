using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Util
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TInput> Concatenar<TInput>(this IEnumerable<IEnumerable<TInput>> enumeraveis)
        {
            IEnumerable<TInput> colecaoVazia = new TInput[0];

            return enumeraveis.Aggregate(colecaoVazia, (atual, enumeravel) => atual.Concat(enumeravel));
        }
    }
}