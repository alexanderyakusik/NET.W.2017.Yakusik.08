using BookLibrary;

namespace BookLibraryExample.Predicates
{
    public class TitleContainsNicePredicate : IPredicate<Book>
    {
        public bool IsTrue(Book item)
        {
            return item.Title.Contains("Nice");
        }
    }
}
