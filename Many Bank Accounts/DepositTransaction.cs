// DepositTransaction.cs

public class DepositTransaction : Transaction
{
    public DepositTransaction(Account account, decimal amount) : base(account, amount)
    {
    }

    public override void Execute()
    {
        _account.Balance += _amount;
    }
}
