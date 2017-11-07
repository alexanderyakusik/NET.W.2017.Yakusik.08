using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace BankAccounts
{
    public sealed class BinaryBankAccountStorage : IBankAccountStorage
    {
        #region Private fields

        private string filepath;

        private List<BankAccount> accounts = new List<BankAccount>();

        #endregion

        #region Interfaces implementations

        #region IBankAccountStorage

        /// <summary>
        /// Creates bank account storage with binary serialization.
        /// </summary>
        /// <param name="filepath">Filepath to be written to / read from.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filepath"/> is null.</exception>
        public BinaryBankAccountStorage(string filepath)
        {
            this.filepath = string.Copy(filepath) ?? throw new ArgumentNullException($"{nameof(filepath)} cannot be null.");
        }

        /// <summary>
        /// Adds <paramref name="account"/> to the list.
        /// </summary>
        /// <param name="account">Account to be added.</param>
        /// <exception cref="ArgumentNullException"><paramref name="account"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="account"/> is already in the list.</exception>
        public void Add(BankAccount account)
        {
            ValidateNullAccount(account);
            ValidateExistingAccount(account);

            accounts.Add(account);
        }

        /// <summary>
        /// Returns the first bank account that matches the <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">Predicate to be used in search.</param>
        /// <returns>First account that matches the predicate. If none found, returns null.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> is null.</exception>
        public BankAccount Find(IPredicate<BankAccount> predicate)
        {
            predicate = predicate ?? throw new ArgumentNullException($"{nameof(predicate)} cannot be null.");

            foreach (BankAccount account in accounts)
            {
                if (predicate.IsTrue(account))
                {
                    return account;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns all the accounts from the list.
        /// </summary>
        /// <returns>All the accounts from the list.</returns>
        public IEnumerable<BankAccount> GetAll()
        {
            return accounts;
        }

        /// <summary>
        /// Removes the specified <paramref name="account"/> from the list.
        /// </summary>
        /// <param name="account">Account to be removed.</param>
        /// <exception cref="ArgumentNullException"><paramref name="account"/> is null.</exception>
        /// <exception cref="ArgumentException"><paramref name="account"/> is not in the list.</exception>
        public void Remove(BankAccount account)
        {
            ValidateNullAccount(account);

            if (!accounts.Contains(account))
            {
                throw new ArgumentException($"{nameof(account)} doesn't exist in the list.");
            }

            accounts.Remove(account);
        }

        /// <summary>
        /// Deletes all the accounts from the list.
        /// </summary>
        public void RemoveAll()
        {
            accounts.Clear();
        }

        /// <summary>
        /// Loads the accounts from the filepath specified in the constructor.
        /// </summary>
        /// <exception cref="ArgumentException">One of the accounts exists in the list.</exception>
        public void Load()
        {
            using (var reader = new BinaryReader(File.Open(filepath, FileMode.Open)))
            {
                BankAccount account = ReadBankAccount(reader);
                ValidateExistingAccount(account);

                accounts.Add(account);
            }
        }

        /// <summary>
        /// Saves all the accounts to the filepath specified in the constructor.
        /// </summary>
        public void Save()
        {
            using (var writer = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                foreach (BankAccount account in accounts)
                {
                    WriteBankAccount(writer, account);
                }
            }
        }

        #endregion

        #endregion

        #region Private methods

        private BankAccount ReadBankAccount(BinaryReader reader)
        {
            string accountType = reader.ReadString();
            int id = reader.ReadInt32();
            string firstName = reader.ReadString();
            string lastName = reader.ReadString();
            decimal balance = reader.ReadDecimal();
            long bonusPoints = reader.ReadInt64();
            bool isClosed = reader.ReadBoolean();

            var account = (BankAccount)Activator.CreateInstance(Type.GetType(accountType), BindingFlags.NonPublic,
                new object[] { id, firstName, lastName, balance, bonusPoints, isClosed });

            return account;
        }

        private void WriteBankAccount(BinaryWriter writer, BankAccount account)
        {
            writer.Write(account.GetType().ToString());
            writer.Write(account.Id);
            writer.Write(account.FirstName);
            writer.Write(account.LastName);
            writer.Write(account.Balance);
            writer.Write(account.BonusPoints);
            writer.Write(account.IsClosed);
        }

        private void ValidateNullAccount(BankAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException($"{nameof(account)} cannot be null.");
            }
        }

        private void ValidateExistingAccount(BankAccount account)
        {
            if (accounts.Contains(account))
            {
                throw new ArgumentException($"This account already exists in the list: {account}");
            }
        }

        #endregion
    }
}
