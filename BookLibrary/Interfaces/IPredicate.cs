namespace BookLibrary
{
    public interface IPredicate<T>
    {
        bool IsTrue(T item);
    }
}
