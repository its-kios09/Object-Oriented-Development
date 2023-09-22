// TransferTransaction.cs

using System;

public class TransferTransaction : Transaction
{
    private Account _toAccount;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(fromAccount, amount)
    {
        _toAccount = toAccount;
    }

    public override void Execute()
    {
        if (_account.Balance >= _amount)
        {
            _account.Balance -= _amount;
            _toAccount.Balance += _amount;
        }
        else
        {
            Console.WriteLine("Insufficient balance.");
        }
    }
}
