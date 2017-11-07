namespace BankAccounts
{
    public interface IPredicate<T>
    {
        bool IsTrue(T item);
    }
}
