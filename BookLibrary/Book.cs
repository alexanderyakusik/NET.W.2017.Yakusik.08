using System;
using System.Globalization;
using BookLibrary.Loggers;
using BookLibrary.Loggers.Adapters;

namespace BookLibrary
{
    public class Book : IComparable<Book>, IComparable, IEquatable<Book>, IFormattable
    {
        #region Private fields

        private static readonly ILogger logger = new NLogAdapter(nameof(Book));

        private int _pagesAmount;
        private decimal _price;

        #endregion

        #region Ctor

        /// <summary>
        /// Creates the book with the specified parameters.
        /// </summary>
        /// <param name="title">Title of the book.</param>
        /// <param name="author">Book's author.</param>
        /// <param name="isbn">International standard book number.</param>
        /// <param name="publishingHouse">House where the book was published.</param>
        /// <param name="publishingYear">Year of book publishment.</param>
        /// <param name="pagesAmount">Amount of pages in the book.</param>
        /// <param name="price">Book's price.</param>
        public Book(
            string title, 
            string author = "", 
            string isbn = "", 
            string publishingHouse = "",
            int publishingYear = 0, 
            int pagesAmount = 0,
            decimal price = 0)
        {
            Title = title ?? string.Empty;
            Author = author ?? string.Empty;
            ISBN = isbn;
            PublishingHouse = publishingHouse ?? string.Empty;
            PublishingYear = publishingYear;
            PagesAmount = pagesAmount;
            Price = price;

            logger.Info($"Created book instance: {this}.");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Title of the book.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Book's author.
        /// </summary>
        public string Author { get; private set; }

        /// <summary>
        /// International standard book number.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Value is less than zero.</exception>
        public string ISBN { get; private set; }

        /// <summary>
        /// House where the book was published.
        /// </summary>
        public string PublishingHouse { get; private set; }

        /// <summary>
        /// Year of book publishment.
        /// </summary>
        public int PublishingYear { get; private set; }

        /// <summary>
        /// Amount of pages in the book.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Value is less than zero.</exception>
        public int PagesAmount
        {
            get
            {
                return _pagesAmount;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(PagesAmount)} cannot be less than zero.");
                }

                _pagesAmount = value;
            }
        }

        /// <summary>
        /// Book's price.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Value is less than zero.</exception>
        public decimal Price
        {
            get
            {
                return _price;
            }

            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(Price)} cannot be less than zero.");
                }

                _price = value;
            }
        }

        #endregion

        #region Overridden operators

        /// <summary>
        /// Checks equality of two books based on all the parameters.
        /// </summary>
        /// <param name="first">First book.</param>
        /// <param name="second">Second book.</param>
        /// <returns>True, if all the parameters are equal. Otherwise, returns false.</returns>
        public static bool operator ==(Book first, Book second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null))
            {
                return false;
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Checks equality of two books based on all the parameters.
        /// </summary>
        /// <param name="first">First book.</param>
        /// <param name="second">Second book.</param>
        /// <returns>False, if all the parameters are equal. Otherwise, returns true.</returns>
        public static bool operator !=(Book first, Book second)
        {
            return !(first == second);
        }

        #endregion

        #region Object overridden methods

        /// <summary>
        /// Checks equality with the <paramref name="obj"/>.
        /// </summary>
        /// <param name="obj">Object to check equality.</param>
        /// <returns>True, if all the parameters are equal. Otherwise, returns false.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return this.Equals(obj as Book);
        }

        /// <summary>
        /// Returns hash code based on all the book parameters.
        /// </summary>
        /// <returns>Book's hash code.</returns>
        public override int GetHashCode()
        {
            const int HASH_INITIAL_SEED = 17;
            const int HASH_ADDITIONAL_SEED = 23;

            int hash;

            unchecked
            {
                hash = HASH_INITIAL_SEED;
                hash = (hash * HASH_ADDITIONAL_SEED) + Title.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + Author.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + ISBN.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + PublishingHouse.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + PublishingYear.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + Price.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + PagesAmount.GetHashCode();
            }

            return hash;
        }

        /// <summary>
        /// Returns string representation of the book parameters.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns string representation of the book parameters based on the <paramref name="format"/>.
        /// </summary>
        /// <param name="format">Specified string formatting.</param>
        /// <returns>Formatted string representation.</returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        #endregion

        #region Interfaces implementations

        #region IComparable

        /// <summary>
        /// Compares the book with the <paramref name="obj"/> based on title.
        /// </summary>
        /// <param name="obj">Object to compare to.</param>
        /// <returns>-1 if the title is lexicographically less, 0 if equal, 1 if greater.
        /// Also, returns 1 if the <paramref name="obj"/> is null.</returns>
        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            return CompareTo(obj as Book);
        }

        #endregion

        #region IComparable<T>

        /// <summary>
        /// Compares the book with the <paramref name="other"/> based on title.
        /// </summary>
        /// <param name="other">Book to compare to.</param>
        /// <returns>-1 if the title is lexicographically less, 0 if equal, 1 if greater.
        /// Also, returns 1 if the <paramref name="other"/> is null.</returns>
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return Title.CompareTo(other.Title);
        }

        #endregion

        #region IEquatable<T>

        /// <summary>
        /// Checks equality with the <paramref name="other"/> based on all the parameters.
        /// </summary>
        /// <param name="other">Book to be checked</param>
        /// <returns>True if all the parameters are equal. Otherwise, returns false.</returns>
        public bool Equals(Book other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            bool result = Title == other.Title &&
                          Author == other.Author &&
                          Price == other.Price &&
                          ISBN == other.ISBN &&
                          PagesAmount == other.PagesAmount &&
                          PublishingYear == other.PublishingYear &&
                          PublishingHouse == other.PublishingHouse;

            return result;
        }

        #endregion

        #region IFormattable

        /// <summary>
        /// Returns string representation of the book parameters based on <paramref name="format"/> and <paramref name="formatProvider"/>.
        /// </summary>
        /// <param name="format">Specified string formatting.</param>
        /// <param name="formatProvider">Culture format provider.</param>
        /// <returns>Formatted string representation.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            format = format.Trim().ToUpperInvariant();
            formatProvider = formatProvider ?? CultureInfo.CurrentCulture;

            switch (format)
            {
                case "S":
                    return $"{Author.ToString(formatProvider)}, " +
                           $"{Title.ToString(formatProvider)}";
                case "C":
                    return $"{Author.ToString(formatProvider)}, " +
                           $"{Title.ToString(formatProvider)}, " +
                           $"\"{PublishingHouse.ToString(formatProvider)}\", " +
                           $"{PublishingYear.ToString(formatProvider)}";
                case "O":
                    return $"ISBN: {ISBN.ToString(formatProvider)}, " +
                           $"{Author.ToString(formatProvider)}, " +
                           $"{Title.ToString(formatProvider)}, " +
                           $"\"{PublishingHouse.ToString(formatProvider)}\", " +
                           $"{PublishingYear.ToString(formatProvider)}, " +
                           $"P. {PagesAmount.ToString(formatProvider)}.";
                case "F":
                    return $"ISBN: {ISBN.ToString(formatProvider)}, " +
                           $"{Author.ToString(formatProvider)}, " +
                           $"{Title.ToString(formatProvider)}, " +
                           $"\"{PublishingHouse.ToString(formatProvider)}\", " +
                           $"{PublishingYear.ToString(formatProvider)}, " +
                           $"P. {PagesAmount.ToString(formatProvider)}., " +
                           $"{Price.ToString(formatProvider)}.";
                default:
                    return $"Title: {Title.ToString(formatProvider)}; " +
                           $"Author: {Author.ToString(formatProvider)}; " +
                           $"Price: {Price.ToString(formatProvider)}; " +
                           $"ISBN: {ISBN.ToString(formatProvider)}; " +
                           $"Publishing house: {PublishingHouse.ToString(formatProvider)}; " +
                           $"Publishing year: {PublishingYear.ToString(formatProvider)}; " +
                           $"Amount of pages: {PagesAmount.ToString(formatProvider)};";
            }
        }

        #endregion

        #endregion
    }
}
