using System;
using System.Collections;
using NUnit.Framework;

namespace BookLibrary.UnitTests
{
    [TestFixture]
    public class BookLibraryTests
    {
        [Test, TestCaseSource(typeof(BookLibraryTestsData), "ToStringTestCases")]
        public string ToString_DifferentFormatStringsPassed_WorksCorrectly(Book book, string formatString)
        {
            return book.ToString(formatString);
        }
        
        [Test, TestCaseSource(typeof(BookLibraryTestsData), "ToStringCustomFormatterTestCases")]
        public string ToString_CustomFormatterPassed_WorksCorrectly(Book book, string formatString, IFormatProvider provider)
        {
            return string.Format(provider, "{0:" + formatString + "}", book);
        }

        private class BookLibraryTestsData
        {
            public static IEnumerable ToStringTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Book("CLR via C#", "Jeffrey Richter", "978-0-7356-6745-7", "Microsoft Press", 2012, 826, 59.99M),
                        "G").Returns(
                        "Title: CLR via C#; Author: Jeffrey Richter; Price: 59,99; " +
                        "ISBN: 978-0-7356-6745-7; Publishing house: Microsoft Press; " +
                        "Publishing year: 2012; Amount of pages: 826;");
                    yield return new TestCaseData(
                        new Book("CLR via C#", "Jeffrey Richter", "978-0-7356-6745-7", "Microsoft Press", 2012, 826, 59.99M),
                        "S").Returns(
                        "Jeffrey Richter, CLR via C#");
                    yield return new TestCaseData(
                        new Book("CLR via C#", "Jeffrey Richter", "978-0-7356-6745-7", "Microsoft Press", 2012, 826, 59.99M),
                        "C").Returns(
                        "Jeffrey Richter, CLR via C#, \"Microsoft Press\", 2012");
                    yield return new TestCaseData(
                        new Book("CLR via C#", "Jeffrey Richter", "978-0-7356-6745-7", "Microsoft Press", 2012, 826, 59.99M),
                        "O").Returns(
                        "ISBN: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#, \"Microsoft Press\", 2012, P. 826.");
                    yield return new TestCaseData(
                        new Book("CLR via C#", "Jeffrey Richter", "978-0-7356-6745-7", "Microsoft Press", 2012, 826, 59.99M),
                        "F").Returns(
                        "ISBN: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#, \"Microsoft Press\", 2012, P. 826., 59,99.");
                }
            }

            public static IEnumerable ToStringCustomFormatterTestCases
            {
                get
                {
                    yield return new TestCaseData(
                        new Book(
                            "CLR via C#", "Jeffrey Richter", "978-0-7356-6745-7", "Microsoft Press", 2012, 826, 59.99M),
                            "S", 
                            new BookFormatter()).Returns(
                        "Jeffrey Richter, CLR via C#, 59,99.");
                    yield return new TestCaseData(
                        new Book(
                            "CLR via C#", "Jeffrey Richter", "978-0-7356-6745-7", "Microsoft Press", 2012, 826, 59.99M),
                            "E",
                            new BookFormatter()).Returns(
                        "ISBN: 978-0-7356-6745-7, Jeffrey Richter, CLR via C#, 59,99.");
                }
            }
        }
    }
}
