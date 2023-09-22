// Bank.cs

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
}
