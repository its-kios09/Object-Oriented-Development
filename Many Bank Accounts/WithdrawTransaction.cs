// WithdrawTransaction.cs

using System;

public class WithdrawTransaction : Transaction
{
    public WithdrawTransaction(Account account, decimal amount) : base(account, amount)
    {
    }

    public override void Execute()
    {
        if (_account.Balance >= _amount)
        {
            _account.Balance -= _amount;
        }
        else
        {
            Console.WriteLine("Insufficient balance.");
        }
    }
}
