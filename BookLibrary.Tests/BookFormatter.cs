using System;

namespace BookLibrary.UnitTests
{
    public class BookFormatter : ICustomFormatter, IFormatProvider
    {
        #region Public methods

        /// <summary>
        /// Formats an <paramref name="arg"/> using <paramref name="format"/> string and <paramref name="formatProvider"/>.
        /// </summary>
        /// <param name="format">Format string.</param>
        /// <param name="arg">Object to represent as string.</param>
        /// <param name="formatProvider">Culture information provider.</param>
        /// <returns>Formatted string representation of <paramref name="arg"/></returns>
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            format = format.Trim().ToUpperInvariant();

            ValidateNullArgument(arg);
            ValidateNullArgument(formatProvider);

            if (!(arg is Book))
            {
                throw new ArgumentException($"{nameof(arg)} must be Book instance.");
            }

            Book book = (Book)arg;

            switch (format)
            {
                case "S":
                    return $"{book.Author.ToString(formatProvider)}, " +
                           $"{book.Title.ToString(formatProvider)}, " +
                           $"{book.Price.ToString(formatProvider)}.";
                case "E":
                    return $"ISBN: {book.ISBN.ToString(formatProvider)}, " +
                           $"{book.Author.ToString(formatProvider)}, " +
                           $"{book.Title.ToString(formatProvider)}, " +
                           $"{book.Price.ToString(formatProvider)}.";
                default:
                    return book.ToString(format, formatProvider);
            }
        }

        /// <summary>
        /// Returns an object that provides culture information and custom formatting.
        /// </summary>
        /// <param name="formatType">Formatting type</param>
        /// <returns>Provider object</returns>
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }

        #endregion

        #region Private methods

        private void ValidateNullArgument<T>(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("Value cannot be null.");
            }
        }

        #endregion
    }
}
