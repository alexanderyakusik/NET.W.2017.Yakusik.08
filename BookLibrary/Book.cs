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

        public string Title { get; private set; }

        public string Author { get; private set; }

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

        public string PublishingHouse { get; private set; }

        public int PublishingYear { get; private set; }

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

        public override string ToString()
        {
            return $"Title: {Title}; Author: {Author}; Price: {Price}; ISBN: {ISBN}; " +
                   $"Publishing house: {PublishingHouse}; Publishing year: {PublishingYear}; " +
                   $"Amount of pages: {PagesAmount};";
        }

        #endregion

        #region Interfaces implementations

        #region IComparable

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

        public int CompareTo(Book other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return Title.CompareTo(other.Title);
        }

        #endregion

        #region IEquatable

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

        public static bool operator !=(Book first, Book second)
        {
            return !(first == second);
        }

        #endregion
    }
}
