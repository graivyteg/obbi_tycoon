public class Wallet
{
    public int Money => _money;
    
    private int _money = 0;

    public Wallet(int amount = 0)
    {
        _money = amount;
    }

    public void AddMoney(int amount)
    {
        _money += amount;
    }

    public bool TryRemoveMoney(int amount)
    {
        if (_money < amount) return false;
        
        _money -= amount;
        return true;
    }
}