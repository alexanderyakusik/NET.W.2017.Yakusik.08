using System;
using System.Collections.Generic;
using System.IO;

namespace BookLibrary
{
    public class BinaryBookStorage : IBookStorage
    {
        #region Private fields

        private string filepath;

        #endregion

        #region Ctors

        /// <summary>
        /// Creates instance of binary book storage.
        /// </summary>
        /// <param name="filepath">File to be read from/written to.</param>
        public BinaryBookStorage(string filepath)
        {
            this.filepath = string.Copy(filepath) ?? throw new ArgumentNullException($"{nameof(filepath)} cannot be null.");
        }

        #endregion

        #region IBookStorage implementation

        /// <summary>
        /// Loads binary serialized books from the filepath specified in ctor.
        /// </summary>
        /// <returns>Book enumeration</returns>
        /// <exception cref="ArgumentException">Specified filepath is not valid.</exception>
        public IEnumerable<Book> Load()
        {
            if (!File.Exists(filepath))
            {
                throw new ArgumentException($"Specified {nameof(filepath)} is not valid.");
            }

            var books = new List<Book>();

            using (BinaryReader reader = new BinaryReader(File.Open(filepath, FileMode.Open)))
            {
                while (reader.BaseStream.Position != reader.BaseStream.Length)
                {
                    books.Add(ReadBook(reader));
                }
            }

            return books;
        }

        /// <summary>
        /// Saves book enumeration to specified filapth using binary serialization.
        /// </summary>
        /// <param name="books">Book enumeration to be written</param>
        public void Save(IEnumerable<Book> books)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                foreach (Book book in books)
                {
                    WriteBook(writer, book);
                }
            }
        }

        #endregion

        #region Private methods

        private void WriteBook(BinaryWriter writer, Book book)
        {
            writer.Write(book.Title);
            writer.Write(book.Author);
            writer.Write(book.Price);
            writer.Write(book.PagesAmount);
            writer.Write(book.ISBN);
            writer.Write(book.PublishingHouse);
            writer.Write(book.PublishingYear);
        }

        private Book ReadBook(BinaryReader reader)
        {
            string title = reader.ReadString();
            string author = reader.ReadString();
            decimal price = reader.ReadDecimal();
            int pagesAmount = reader.ReadInt32();
            string isbn = reader.ReadString();
            string publishingHouse = reader.ReadString();
            int publishingYear = reader.ReadInt32();

            return new Book(title, author, isbn, publishingHouse, publishingYear, pagesAmount, price);
        }

        #endregion
    }
}
