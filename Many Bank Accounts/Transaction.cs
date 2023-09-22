// Transaction.cs

public abstract class Transaction
{
    protected Account _account;
    protected decimal _amount;

    public Transaction(Account account, decimal amount)
    {
        _account = account;
        _amount = amount;
    }

    public abstract void Execute();
}
