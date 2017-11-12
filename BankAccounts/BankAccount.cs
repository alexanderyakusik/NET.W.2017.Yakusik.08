using System;

namespace BankAccounts
{
    public abstract class BankAccount : IEquatable<BankAccount>
    {
        #region Private fields

        private static readonly int ReplenishBonusBalanceCoeff = 3;
        private static readonly int ReplenishBonusReplenishCoeff = 5;

        private static readonly int WithdrawPenaltyBalanceCoeff = 2;
        private static readonly int WithdrawPenaltyReplenishCoeff = 3;

        private decimal _balance;
        private string _firstName;
        private string _lastName;
        private long _bonusPoints;

        #endregion  

        #region Ctors

        /// <summary>
        /// Creates bank account with zero balance and bonus points.
        /// </summary>
        /// <param name="id">Client's id.</param>
        /// <param name="firstName">First name of the account's owner.</param>
        /// <param name="lastName">Last name of the account's owner.</param>
        protected BankAccount(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>
        /// Creates bank account with specified balance and bonus points.
        /// </summary>
        /// <param name="id">Client's id.</param>
        /// <param name="firstName">First name of the account's owner.</param>
        /// <param name="lastName">Last name of the account's owner.</param>
        /// <param name="balance">Account balance.</param>
        /// <param name="bonusPoints">Account bonus points.</param>
        /// <param name="isClosed">Account status.</param>
        protected BankAccount(
            int id, string firstName, string lastName, decimal balance, long bonusPoints, bool isClosed) : this(id, firstName, lastName)
        {
            Balance = balance;
            BonusPoints = bonusPoints;
            IsClosed = isClosed;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Account's identifier number.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// First name of the account's owner.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public string FirstName
        {
            get => _firstName;
            private set => _firstName = value ?? throw new ArgumentNullException($"{nameof(FirstName)} cannot be null.");
        }

        /// <summary>
        /// Last name of the account's owner.
        /// </summary>
        /// <exception cref="ArgumentNullException">Value is null.</exception>
        public string LastName
        {
            get => _lastName;
            private set => _lastName = value ?? throw new ArgumentNullException($"{nameof(LastName)} cannot be null.");
        }

        /// <summary>
        /// Current balance on the account.
        /// </summary>
        /// <exception cref="InvalidOperationException">Value is less than zero.</exception>
        public decimal Balance
        {
            get
            {
                return _balance;
            }

            private set
            {
                _balance = value >= 0 ?
                    value : throw new InvalidOperationException($"{nameof(Balance)} is less than zero.");
            }
        }

        /// <summary>
        /// Current bonus points on the account.
        /// </summary>
        public long BonusPoints
        {
            get => _bonusPoints;
            private set => _bonusPoints = value >= 0 ? value : 0;
        }

        /// <summary>
        /// Account's closed status.
        /// </summary>
        public bool IsClosed { get; private set; }

        /// <summary>
        /// Bonus points acquired when replenishing account.
        /// </summary>
        public int ReplenishBonus
        {
            get => (ReplenishBonusBalanceCoeff * BalanceValue) +
                   (ReplenishBonusReplenishCoeff * ReplenishValue);
        }

        /// <summary>
        /// Bonus points removed when withdrawing from account.
        /// </summary>
        public int WithdrawPenalty
        {
            get => (WithdrawPenaltyBalanceCoeff * BalanceValue) +
                   (WithdrawPenaltyReplenishCoeff * ReplenishValue);
        }

        /// <summary>
        /// Value of the balance. Depends on the account type.
        /// </summary>
        public abstract int BalanceValue { get; }

        /// <summary>
        /// Value of the account replenishing. Depends on the account type.
        /// </summary>
        public abstract int ReplenishValue { get; }

        #endregion

        #region Overridden operators

        /// <summary>
        /// Checks equality of two bank accounts based on all the parameters.
        /// </summary>
        /// <param name="first">First bank account.</param>
        /// <param name="second">Second bank account.</param>
        /// <returns>True, if all the parameters are equal. Otherwise, returns false.</returns>
        public static bool operator ==(BankAccount first, BankAccount second)
        {
            if (ReferenceEquals(first, second))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Checks equality of two bank accounts based on all the parameters.
        /// </summary>
        /// <param name="first">First bank account.</param>
        /// <param name="second">Second bank account.</param>
        /// <returns>False, if all the parameters are equal. Otherwise, returns true.</returns>
        public static bool operator !=(BankAccount first, BankAccount second)
        {
            return !(first == second);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Closes account and disables all the money operations.
        /// </summary>
        public void Close()
        {
            IsClosed = true;
        }

        /// <summary>
        /// Adds money on the account.
        /// </summary>
        /// <param name="amount">Amount of money to add.</param>
        /// <exception cref="InvalidOperationException">Account is closed.</exception>
        public void Replenish(decimal amount)
        {
            if (IsClosed)
            {
                throw new InvalidOperationException($"Cannot replenish closed account.");
            }

            Balance += amount;
            BonusPoints += ReplenishBonus;
        }

        /// <summary>
        /// Withdraws money from the account.
        /// </summary>
        /// <param name="amount">Amount of money to be withdrawn.</param>
        /// <exception cref="InvalidOperationException">Account is closed.</exception>
        public void Withdraw(decimal amount)
        {
            if (IsClosed)
            {
                throw new InvalidOperationException($"Cannot withdraw from closed account.");
            }

            Balance -= amount;
            BonusPoints -= WithdrawPenalty;
        }

        #endregion

        #region Interfaces implementations

        #region IEquatable<T>

        /// <summary>
        /// Checks equality with the <paramref name="other"/> based on all the parameters.
        /// </summary>
        /// <param name="other">Account to be checked</param>
        /// <returns>True if all the parameters are equal. Otherwise, returns false.</returns>
        public bool Equals(BankAccount other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (ReferenceEquals(other, null))
            {
                return false;
            }

            bool result = Id == other.Id &&
                          FirstName == other.FirstName &&
                          LastName == other.LastName &&
                          Balance == other.Balance &&
                          BonusPoints == other.BonusPoints;

            return result;
        }

        #endregion

        #endregion

        #region Object overridden methods

        /// <summary>
        /// Checks equality with the <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">Object to check equality.</param>
        /// <returns>True, if all the parameters are equal. Otherwise, returns false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return this.Equals(obj as BankAccount);
        }

        /// <summary>
        /// Returns hash code based on all the account parameters.
        /// </summary>
        /// <returns>Account's hash code.</returns>
        public override int GetHashCode()
        {
            const int HASH_INITIAL_SEED = 17;
            const int HASH_ADDITIONAL_SEED = 23;

            int hash = HASH_INITIAL_SEED;
            unchecked
            {
                hash = (hash * HASH_ADDITIONAL_SEED) + Id.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + FirstName.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + LastName.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + Balance.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) + BonusPoints.GetHashCode();
                hash = (hash * HASH_ADDITIONAL_SEED) * IsClosed.GetHashCode();
            }

            return hash;
        }

        /// <summary>
        /// Returns string representation of the account's parameters.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            return $"Id: {Id}; First name: {FirstName}; Last name: {LastName}; " +
                   $"Account type: {GetType().Name}; Balance: {Balance}; Bonus points: {BonusPoints}; " +
                   $"Closed: {IsClosed}.";
        }

        #endregion
    }
}
