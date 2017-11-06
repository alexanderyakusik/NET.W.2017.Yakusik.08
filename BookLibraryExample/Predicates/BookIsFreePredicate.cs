using BookLibrary;

namespace BookLibraryExample.Predicates
{
    public class BookIsFreePredicate : IPredicate<Book>
    {
        public bool IsTrue(Book item)
        {
            return item.Price == 0;
        }
    }
}
