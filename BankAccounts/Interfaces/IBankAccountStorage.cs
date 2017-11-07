using System.Collections.Generic;

namespace BankAccounts
{
    public interface IBankAccountStorage
    {
        void Add(BankAccount account);

        void Remove(BankAccount account);

        IEnumerable<BankAccount> GetAll();

        void RemoveAll();

        BankAccount Find(IPredicate<BankAccount> predicate);

        void Save();

        void Load();
    }
}
