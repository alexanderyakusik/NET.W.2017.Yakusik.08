using System.Collections.Generic;
using BookLibrary;

namespace BookLibraryExample.Comparers
{
    public class TitleComparer : IComparer<Book>
    {
        public int Compare(Book first, Book second)
        {
            return first.Title.CompareTo(second.Title);
        }
    }
}
