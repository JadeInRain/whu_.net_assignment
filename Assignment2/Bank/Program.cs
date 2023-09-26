using System;

// 1.定义账号类
public class Account
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }

    public virtual void Withdraw(decimal amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
        }
        else
        {
            throw new InvalidOperationException("Insufficient balance");
        }
    }
}

// 2.定义信用账号类
public class CreditAccount : Account
{
    public decimal CreditLimit { get; set; }

    public override void Withdraw(decimal amount)
    {
        if (Balance + CreditLimit >= amount)
        {
            Balance -= amount;
        }
        else
        {
            throw new InvalidOperationException("Insufficient balance and credit");
        }
    }
}

// 3.定义取走大笔金额事件的参数类
public class BigMoneyArgs : EventArgs
{
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
}

// 4.定义ATM类
public class ATM
{
    // 定义事件和委托
    public delegate void BigMoneyFetchedHandler(object sender, BigMoneyArgs e);
    public event BigMoneyFetchedHandler BigMoneyFetched;

    // 定义取款方法
    public void Withdraw(Account account, decimal amount)
    {
        if (amount > 10000)
        {
            // 激活事件
            BigMoneyFetched?.Invoke(this, new BigMoneyArgs { AccountNumber = account.AccountNumber, Amount = amount });
        }

        // 模拟坏钞率为30%的情况
        Random random = new Random();
        if (random.NextDouble() < 0.3)
        {
            throw new BadCashException("Bad cash detected");
        }

        account.Withdraw(amount);
    }
}

// 5.定义自定义异常类
public class BadCashException : Exception
{
    public BadCashException(string message) : base(message) { }
}

// 主程序
class Program
{
    static void Main()
    {
        ATM atm = new ATM();
        atm.BigMoneyFetched += Atm_BigMoneyFetched;

        Account account = new CreditAccount { AccountNumber = "12345", Balance = 15000, CreditLimit = 5000 };

        try
        {
            atm.Withdraw(account, 11000);
        }
        catch (BadCashException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void Atm_BigMoneyFetched(object sender, BigMoneyArgs e)
    {
        Console.WriteLine($"Notice: A large amount of money was fetched from account {e.AccountNumber}. Amount: {e.Amount}");
    }
}
