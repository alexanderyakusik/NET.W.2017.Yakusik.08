using BookLibrary;
using System.Collections.Generic;

namespace BookLibraryExample.Comparers
{
    public class PriceComparer : IComparer<Book>
    {
        public int Compare(Book first, Book second)
        {
            return first.Price.CompareTo(second.Price);
        }
    }
}
