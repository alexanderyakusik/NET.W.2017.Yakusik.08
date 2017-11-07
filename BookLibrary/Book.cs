using System;

namespace BookLibrary
{
    public class Book : IComparable<Book>, IComparable, IEquatable<Book>
    {
        #region Private fields

        private long _isbn;
        private int _pagesAmount;
        private decimal _price;

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
        public long ISBN
        {
            get { return _isbn; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"{nameof(ISBN)} cannot be less than zero.");
                }

                _isbn = value;
            }
        }

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
            get { return _pagesAmount; }
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
            get { return _price; }
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
        public Book(string title, string author = "", long isbn = 0, string publishingHouse = "",
                    int publishingYear = 0, int pagesAmount = 0, decimal price = 0)
        {
            Title = title ?? string.Empty;
            Author = author ?? string.Empty;
            ISBN = isbn;
            PublishingHouse = publishingHouse ?? string.Empty;
            PublishingYear = publishingYear;
            PagesAmount = PagesAmount;
            Price = price;
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

            return Equals(obj as Book);
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
                hash = hash * HASH_ADDITIONAL_SEED + Title.GetHashCode();
                hash = hash * HASH_ADDITIONAL_SEED + Author.GetHashCode();
                hash = hash * HASH_ADDITIONAL_SEED + ISBN.GetHashCode();
                hash = hash * HASH_ADDITIONAL_SEED + PublishingHouse.GetHashCode();
                hash = hash * HASH_ADDITIONAL_SEED + PublishingYear.GetHashCode();
                hash = hash * HASH_ADDITIONAL_SEED + Price.GetHashCode();
                hash = hash * HASH_ADDITIONAL_SEED + PagesAmount.GetHashCode();
            }

            return hash;
        }

        /// <summary>
        /// Returns string representation of the book parameters.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return $"Title: {Title}; Author: {Author}; Price: {Price}; ISBN: {ISBN}; " +
                   $"Publishing house: {PublishingHouse}; Publishing year: {PublishingYear}; " +
                   $"Amount of pages: {PagesAmount};";
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
    }
}
