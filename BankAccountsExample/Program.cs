using BankAccounts;
using System;

namespace BankAccountsExample
{
    class Program
    {
        static void Main(string[] args)
        {
            BankAccount first = new PlatinumBankAccount(1, "Alexander", "Yakusik");
            BankAccount second = new GoldBankAccount(2, "Daniil", "Gasyul");

            #region BankAccount operations

            Console.WriteLine($"First account: {first}");
            Console.WriteLine($"\nSecond account: {second}");

            Console.WriteLine($"\nFirst account replenish bonus: {first.ReplenishBonus}, " +
                              $"withdraw penalty: {first.WithdrawPenalty}");
            Console.WriteLine($"Second account replenish bonus: {second.ReplenishBonus}, " +
                              $"withdraw penalty: {second.WithdrawPenalty}");

            Console.WriteLine("\nReplenishing each account with 2000.");
            first.Replenish(2000);
            second.Replenish(2000);

            Console.WriteLine($"\nFirst account bonus points: {first.BonusPoints}");
            Console.WriteLine($"Second account bonus points: {second.BonusPoints}");

            Console.WriteLine($"\nTrying to withdraw more money than there actually is.");

            try
            {
                first.Withdraw(3000);
                Console.WriteLine("Successfully withdrawn 3000.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Unable to withdraw 3000.");
            }

            Console.WriteLine("\nWithdrawing 1500 from each account.");
            first.Withdraw(1500);
            second.Withdraw(1500);

            Console.WriteLine($"\nFirst account bonus points: {first.BonusPoints}");
            Console.WriteLine($"Second account bonus points: {second.BonusPoints}");

            Console.WriteLine("\nClosing first account");
            first.Close();

            Console.WriteLine("\nTrying to replenish closed account with 100.");
            try
            {
                first.Replenish(100);
                Console.WriteLine("Replenished account successfully.");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Couldn't replenish closed account.");
            }

            #endregion

            #region Storage operations

            IBankAccountStorage storage = new BinaryBankAccountStorage("storage.data");

            Console.WriteLine("\n\nAdding first account to the storage.");
            storage.Add(first);

            Console.WriteLine("\nTrying to add first account once again.");
            try
            {
                storage.Add(first);
                Console.WriteLine("Successfully added first account once again.");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Cannot add existing account.");
            }

            Console.WriteLine("\nSaving all accounts to storage.");
            storage.Save();

            Console.WriteLine("Removing all accounts from the list.");
            storage.RemoveAll();

            Console.WriteLine("Loading accounts from storage.");
            storage.Load();

            Console.WriteLine("Loaded accounts:");
            foreach (BankAccount account in storage.GetAll())
            {
                Console.WriteLine($"\n{account}");
            }

            #endregion

            Console.ReadLine();
        }
    }
}
