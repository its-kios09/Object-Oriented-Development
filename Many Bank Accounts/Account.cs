// Account.cs

public class Account
{
    private string _name;
    private decimal _balance;

    public string Name
    {
        get { return _name; }
    }

    public decimal Balance
    {
        get { return _balance; }
        set { _balance = value; }
    }

    public Account(string name)
    {
        _name = name;
        _balance = 0;
    }
}
