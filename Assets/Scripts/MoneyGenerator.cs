using UnityEngine;
using Zenject;

public class MoneyGenerator : ITickable
{
    public float MoneyPerSecond { get; private set; }
    public Wallet Wallet { get; private set; }
    
    private float _timer = 0;

    public MoneyGenerator()
    {
        Wallet = new Wallet();
        MoneyPerSecond = 1;
    }

    public void Tick()
    {
        _timer += Time.deltaTime;

        if (_timer >= 1)
        {
            Wallet.AddMoney(Mathf.FloorToInt(MoneyPerSecond));
            _timer = 0;
        }
    }

    public void AddMoneyPerSecond(float amount)
    {
        MoneyPerSecond += amount;
    }

    public void SetMoneyPerSecond(float amount)
    {
        MoneyPerSecond = amount;
    }
    
    public void Withdraw(Wallet wallet)
    {
        var money = Wallet.Money;
        Wallet.TryRemoveMoney(money);
        wallet.AddMoney(money);
    }


}