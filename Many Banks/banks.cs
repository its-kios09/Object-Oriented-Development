using System;
using System.Collections.Generic;

public class Bank
{
    private List<Account> _accounts;

    public Bank()
    {
        _accounts = new List<Account>();
    }

    public void AddAccount(Account account)
    {
        _accounts.Add(account);
    }

    public Account GetAccount(string name)
    {
        foreach (Account account in _accounts)
        {
            if (account.Name == name)
            {
                return account;
            }
        }
        return null;
    }

    public void ExecuteTransaction(Transaction transaction)
    {
        transaction.Execute();
    }
}

public class Account
{
    public string Name { get; }
    public decimal Balance { get; set; }

    public Account(string name, decimal balance)
    {
        Name = name;
        Balance = balance;
    }
}

public abstract class Transaction
{
    protected Account _account;

    public Transaction(Account account)
    {
        _account = account;
    }

    public abstract void Execute();
    public abstract void Print();
}

public class DepositTransaction : Transaction
{
    private decimal _amount;

    public DepositTransaction(Account account, decimal amount) : base(account)
    {
        _amount = amount;
    }

    public override void Execute()
    {
        _account.Balance += _amount;
    }

    public override void Print()
    {
        Console.WriteLine($"Deposited {_amount} into account {_account.Name}");
    }
}

public class WithdrawTransaction : Transaction
{
    private decimal _amount;

    public WithdrawTransaction(Account account, decimal amount) : base(account)
    {
        _amount = amount;
    }

    public override void Execute()
    {
        if (_account.Balance >= _amount)
        {
            _account.Balance -= _amount;
        }
        else
        {
            Console.WriteLine("Insufficient funds");
        }
    }

    public override void Print()
    {
        Console.WriteLine($"Withdrawn {_amount} from account {_account.Name}");
    }
}

public class TransferTransaction : Transaction
{
    private Account _toAccount;
    private decimal _amount;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(fromAccount)
    {
        _toAccount = toAccount;
        _amount = amount;
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
            Console.WriteLine("Insufficient funds");
        }
    }

    public override void Print()
    {
        Console.WriteLine($"Transferred {_amount} from account {_account.Name} to account {_toAccount.Name}");
    }
}

public class Program
{
    private static Bank _bank;

    public static void Main(string[] args)
    {
        _bank = new Bank();

        MenuOption newAccountOption = new MenuOption("NewAccount", "Create a new account", CreateNewAccount);
        MenuOption depositOption = new MenuOption("Deposit", "Deposit money into an account", DoDeposit);
        MenuOption withdrawOption = new MenuOption("Withdraw", "Withdraw money from an account", DoWithdraw);
        MenuOption transferOption = new MenuOption("Transfer", "Transfer money between accounts", DoTransfer);
        MenuOption exitOption = new MenuOption("Exit", "Exit the program", ExitProgram);

        Menu menu = new Menu();
        menu.AddOption(newAccountOption);
        menu.AddOption(depositOption);
        menu.AddOption(withdrawOption);
        menu.AddOption(transferOption);
        menu.AddOption(exitOption);

        menu.Run();
    }

    private static void CreateNewAccount()
    {
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();
        Console.Write("Enter starting balance: ");
        decimal balance = decimal.Parse(Console.ReadLine());
        Account newAccount = new Account(name, balance);
        _bank.AddAccount(newAccount);
        Console.WriteLine("Account created successfully!");
    }

    private static void DoDeposit()
    {
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();
        Account account = _bank.GetAccount(name);
        if (account == null)
        {
            Console.WriteLine($"No account found with name {name}");
            return;
        }

        Console.Write("Enter the amount to deposit: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        DepositTransaction depositTransaction = new DepositTransaction(account, amount);
        _bank.ExecuteTransaction(depositTransaction);

        depositTransaction.Print();
    }

    private static void DoWithdraw()
    {
        Console.Write("Enter account name: ");
        string name = Console.ReadLine();
        Account account = _bank.GetAccount(name);
        if (account == null)
        {
            Console.WriteLine($"No account found with name {name}");
            return;
        }

        Console.Write("Enter the amount to withdraw: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        WithdrawTransaction withdrawTransaction = new WithdrawTransaction(account, amount);
        _bank.ExecuteTransaction(withdrawTransaction);

        withdrawTransaction.Print();
    }

    private static void DoTransfer()
    {
        Console.Write("Enter account name to transfer from: ");
        string fromName = Console.ReadLine();
        Account fromAccount = _bank.GetAccount(fromName);
        if (fromAccount == null)
        {
            Console.WriteLine($"No account found with name {fromName}");
            return;
        }

        Console.Write("Enter account name to transfer to: ");
        string toName = Console.ReadLine();
        Account toAccount = _bank.GetAccount(toName);
        if (toAccount == null)
        {
            Console.WriteLine($"No account found with name {toName}");
            return;
        }

        Console.Write("Enter the amount to transfer: ");
        decimal amount = decimal.Parse(Console.ReadLine());

        TransferTransaction transferTransaction = new TransferTransaction(fromAccount, toAccount, amount);
        _bank.ExecuteTransaction(transferTransaction);

        transferTransaction.Print();
    }

    private static void ExitProgram()
    {
        Console.WriteLine("Exiting the program...");
        Environment.Exit(0);
    }
}

public class MenuOption
{
    public string Name { get; }
    public string Description { get; }
    public Action Action { get; }

    public MenuOption(string name, string description, Action action)
    {
        Name = name;
        Description = description;
        Action = action;
    }
}

public class Menu
{
    private List<MenuOption> _options;

    public Menu()
    {
        _options = new List<MenuOption>();
    }

    public void AddOption(MenuOption option)
    {
        _options.Add(option);
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Menu:");
            for (int i = 0; i < _options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_options[i].Name} - {_options[i].Description}");
            }

            Console.Write("Enter option number: ");
            int optionNumber;
            if (!int.TryParse(Console.ReadLine(), out optionNumber))
            {
                Console.WriteLine("Invalid option number");
                continue;
            }

            if (optionNumber >= 1 && optionNumber <= _options.Count)
            {
                _options[optionNumber - 1].Action.Invoke();
            }
            else
            {
                Console.WriteLine("Invalid option number");
            }

            Console.WriteLine();
        }
    }
}
