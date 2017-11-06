using System;
using System.Collections.Generic;

namespace BookLibrary
{
    public class BookListService : IBookService
    {
        #region Private fields

        private List<Book> books = new List<Book>();

        #endregion

        #region Interfaces implementation

        #region IBookService

        /// <summary>
        /// Add a <paramref name="book"/> to the list.
        /// </summary>
        /// <param name="book">Book to be added.</param>
        /// <exception cref="ArgumentException">Book already exists in the list.</exception>
        public void AddBook(Book book)
        {
            ValidateNullBook(book);

            if (books.Contains(book))
            {
                throw new ArgumentException($"{nameof(book)} already exists in the list.");
            }

            books.Add(book);
        }

        /// <summary>
        /// Finds first book in the list that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Specified predicate to search book.</param>
        /// <returns>First book that matches the <paramref name="predicate"/>. Otherwise, returns null.</returns>
        public Book FindBookByTag(IPredicate<Book> predicate)
        {
            foreach (Book book in books)
            {
                if (predicate.IsTrue(book))
                {
                    return book;
                }
            }

            return null;
        }

        /// <summary>
        /// Get all books in the list.
        /// </summary>
        /// <returns>Book enumeration from the list.</returns>
        public IEnumerable<Book> GetBooks()
        {
            return books;
        }

        /// <summary>
        /// Loads all the books from the <paramref name="storage"/> that are not currently in the list.
        /// </summary>
        /// <param name="storage">Storage to be loaded from</param>
        public void LoadBooksFromStorage(IBookStorage storage)
        {
            foreach (Book book in storage.Load())
            {
                if (!books.Contains(book))
                {
                    books.Add(book);
                }
            }
        }

        /// <summary>
        /// Removes <paramref name="book"/> from the list.
        /// </summary>
        /// <param name="book">Book to be deleted.</param>
        /// <exception cref="ArgumentException">No such book found in the list.</exception>
        public void RemoveBook(Book book)
        {
            ValidateNullBook(book);

            if (!books.Contains(book))
            {
                throw new ArgumentException($"{nameof(book)} doesn't exist in the list.");
            }

            books.Remove(book);
        }

        /// <summary>
        /// Removes all the books from the list.
        /// </summary>
        public void RemoveAllBooks()
        {
            books.Clear();
        }

        /// <summary>
        /// Saves all the books from the list to the <paramref name="storage"/>.
        /// </summary>
        /// <param name="storage">Storage to be written to.</param>
        public void SaveBooksToStorage(IBookStorage storage)
        {
            storage.Save(GetBooks());
        }

        /// <summary>
        /// Sorts the list of books using <paramref name="comparer"/>.
        /// </summary>
        /// <param name="comparer">Instance to compare two books.</param>
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            books.Sort(comparer);
        }

        #endregion

        #endregion

        #region Private methods

        private void ValidateNullBook(Book book)
        {
            if (ReferenceEquals(book, null))
            {
                throw new ArgumentNullException($"{nameof(book)} cannot be null.");
            }
        }

        #endregion
    }
}
