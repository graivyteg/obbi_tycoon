using System;

public class Wallet
{
    public int Money => _money;
    
    private int _money = 0;
    
    public event Action<int> OnBalanceChanged;

    public Wallet(int amount = 0)
    {
        _money = amount;
    }

    public void AddMoney(int amount)
    {
        _money += amount;
        OnBalanceChanged?.Invoke(_money);
    }

    public bool TryRemoveMoney(int amount)
    {
        if (_money < amount) return false;
        
        _money -= amount;
        OnBalanceChanged?.Invoke(_money);
        return true;
    }
}