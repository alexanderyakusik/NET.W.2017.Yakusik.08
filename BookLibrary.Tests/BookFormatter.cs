using System;

namespace BookLibrary.UnitTests
{
    public class BookFormatter : ICustomFormatter, IFormatProvider
    {
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "G";
            }

            format = format.Trim().ToUpperInvariant();

            arg = arg ?? throw new ArgumentNullException($"{nameof(arg)} cannot be null.");
            formatProvider = formatProvider ?? throw new ArgumentNullException($"{nameof(formatProvider)} cannot be null.");

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

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }
    }
}
