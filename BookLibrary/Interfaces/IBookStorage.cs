using System.Collections.Generic;

namespace BookLibrary
{
    public interface IBookStorage
    {
        void Save(IEnumerable<Book> books);

        IEnumerable<Book> Load();
    }
}
