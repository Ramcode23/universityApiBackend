using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqSnippets
{
    public static class Paginator
    {
        static public IQueryable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPage)
        {
            int startIndex = (pageNumber - 1) * resultsPage;
            return collection.Skip(startIndex).Take(resultsPage).AsQueryable();

        }
    }
}
