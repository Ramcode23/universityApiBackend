using System;
using System.Collections.Generic;
using System.Linq;
namespace LinqSnippets
{
    public class Snippets
    {
     static  public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPage)
        {
            int startIndex = (pageNumber - 1) * resultsPage;
            return collection.Skip(startIndex).Take(resultsPage);

        }

        // Variable
        static public void LingVariables()
        {
            int[] numbers = {1,2,3,4,5,6,7,8,9,10};

            var aboveAverage= from  number in numbers
                               let average= numbers.Average()
                               let nSquared=Math.Pow(number, 2)
                               where nSquared>average
                                select number;

            foreach (var number in aboveAverage)
            {
                Console.WriteLine("Query {0} {1}", number, Math.Pow(number, 2));
            }
        }

        // Zip 
        static public void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5};
            string[] stringNumber = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumber= numbers.Zip(stringNumber,(number,word)=>number+"="+word);
        }

         static public void repeatRangeLinq()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 1000);
            IEnumerable<string> fivex = Enumerable.Repeat("x", 5);
        }

        static public void agregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int sum= numbers.Aggregate( (previSum,current)=>previSum+current);
        }
    }
}

