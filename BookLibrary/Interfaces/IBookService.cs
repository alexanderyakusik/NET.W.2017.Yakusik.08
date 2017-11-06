using System.Collections.Generic;

namespace BookLibrary
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();

        void AddBook(Book book);

        void RemoveBook(Book book);

        Book FindBookByTag(IPredicate<Book> predicate);

        void SortBooksByTag(IComparer<Book> comparer);

        void RemoveAllBooks();

        void LoadBooksFromStorage(IBookStorage storage);

        void SaveBooksToStorage(IBookStorage storage);
    }
}
