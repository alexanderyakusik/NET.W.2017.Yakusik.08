using BookLibrary;
using BookLibraryExample.Comparers;
using BookLibraryExample.Predicates;
using System;

namespace BookLibraryExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var first = new Book("Nice Book");
            var second = new Book("A Very Nice Book");
            var secondEqual = new Book("A Very Nice Book");

            #region Book operations

            Console.WriteLine($"First book: {first}");
            Console.WriteLine($"Second book: {second}");
            Console.WriteLine($"Third book: {secondEqual}");

            Console.WriteLine($"\nFirst equals second: {first == second}");
            Console.WriteLine($"Second equals third: {second == secondEqual}");

            Console.WriteLine($"Second and third have same hash codes: {second.GetHashCode() == secondEqual.GetHashCode()}");

            Console.WriteLine($"First book is greater than second book: {first.CompareTo(second) == 1}");
            Console.WriteLine($"Second book is in the same position as third: {second.CompareTo(secondEqual) == 0}");

            #endregion

            #region BookListService operations

            IBookService bookService = new BookListService();

            #region Without storage

            Console.WriteLine($"\nAdding first book");
            bookService.AddBook(first);

            try
            {
                bookService.AddBook(first);
                Console.WriteLine($"Added first book once more");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Couldn't add first book once more");
            }

            Console.WriteLine($"\nAdding second book");
            bookService.AddBook(second);

            try
            {
                bookService.AddBook(secondEqual);
                Console.WriteLine($"Added third book that is equal to second");
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"Couldn't add third book that is equal to second");
            }

            bookService.SortBooksByTag(new TitleComparer());
            bookService.SortBooksByTag(new PriceComparer());

            var freeBook = bookService.FindBookByTag(new BookIsFreePredicate());
            Console.WriteLine($"\nBook that is free: {freeBook}");

            var niceBook = bookService.FindBookByTag(new TitleContainsNicePredicate());
            Console.WriteLine($"Book which title contains word 'Nice': {niceBook}");

            #endregion

            #region With storage

            IBookStorage bookStorage = new BinaryBookStorage("storage.data");

            Console.WriteLine($"\nBooks in list before storaging:");
            foreach (var book in bookService.GetBooks())
            {
                Console.WriteLine(book);
            }
            bookService.SaveBooksToStorage(bookStorage);

            bookService.RemoveAllBooks();
            Console.WriteLine($"\nRemoved all books from list.");

            bookService.LoadBooksFromStorage(bookStorage);
            bookService.LoadBooksFromStorage(bookStorage);

            Console.WriteLine($"\nBooks in list after loading from storage:");
            foreach (var book in bookService.GetBooks())
            {
                Console.WriteLine(book);
            }

            #endregion

            #endregion

            Console.ReadLine();
        }
    }
}
