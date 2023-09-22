// Program.cs

using System;

public class Program
{
    private static Bank _bank;

    public static void Main()
    {
        _bank = new Bank();

        while (true)
        {
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Enter account name: ");
                    string name = Console.ReadLine();
                    Account newAccount = new Account(name);
                    _bank.AddAccount(newAccount);
                    Console.WriteLine("Account created successfully.");
                    break;
                case 2:
                    Account depositAccount = FindAccount(_bank);
                    if (depositAccount != null)
                    {
                        Console.Write("Enter the amount to deposit: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        DepositTransaction depositTransaction = new DepositTransaction(depositAccount, depositAmount);
                        depositTransaction.Execute();
                        Console.WriteLine("Deposit successful.");
                    }
                    break;
                case 3:
                    Account withdrawAccount = FindAccount(_bank);
                    if (withdrawAccount != null)
                    {
                        Console.Write("Enter the amount to withdraw: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        WithdrawTransaction withdrawTransaction = new WithdrawTransaction(withdrawAccount, withdrawAmount);
                        withdrawTransaction.Execute();
                        Console.WriteLine("Withdrawal successful.");
                    }
                    break;
                case 4:
                    Account fromAccount = FindAccount(_bank);
                    if (fromAccount != null)
                    {
                        Console.Write("Enter the account to transfer to: ");
                        Account toAccount = FindAccount(_bank);
                        if (toAccount != null)
                        {
                            Console.Write("Enter the amount to transfer: ");
                            decimal transferAmount = decimal.Parse(Console.ReadLine());
                            TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, transferAmount);
                            transferTransaction.Execute();
                            Console.WriteLine("Transfer successful.");
                        }
                    }
                    break;
                case 5:
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private static Account FindAccount(Bank fromBank)
    {
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();
        Account result = fromBank.GetAccount(name);
        if (result == null)
        {
            Console.WriteLine($"No account found with name {name}");
        }
        return result;
    }
}
