namespace BankAccounts
{
    public sealed class BaseBankAccount : BankAccount
    {
        #region Private fields

        private static readonly int DefaultBalanceValue = 5;
        private static readonly int DefaultReplenishValue = 2;

        #endregion

        #region Ctors

        /// <summary>
        /// Creates a bank account with zero balance and bonus points and sets the status to base.
        /// </summary>
        /// <param name="id">Client's id.</param>
        /// <param name="firstName">First name of the account's owner.</param>
        /// <param name="lastName">Last name of the account's owner.</param>
        public BaseBankAccount(int id, string firstName, string lastName) : base(id, firstName, lastName)
        {
        }

        public BaseBankAccount(
            int id, string firstName, string lastName, decimal balance, long bonusPoints, bool isClosed) : 
            base(id, firstName, lastName, balance, bonusPoints, isClosed)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Value of the balance. Depends on the account type.
        /// </summary>
        public override int BalanceValue => DefaultBalanceValue;

        /// <summary>
        /// Value of the account replenishing. Depends on the account type.
        /// </summary>
        public override int ReplenishValue => DefaultReplenishValue;

        #endregion
    }
}
