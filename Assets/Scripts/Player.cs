using System;
using UnityEngine;
using YG;
using Zenject;

public class Player : MonoYandex
{
    [Inject] private GameSaver _saver;

    public Wallet Wallet = new Wallet();

    protected override void OnSDK()
    {
        Debug.Log("Data Loaded: Money - " + YandexGame.savesData.money);
        Wallet.AddMoney(YandexGame.savesData.money - Wallet.Money);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        Wallet.OnBalanceChanged += SaveBalance;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Wallet.OnBalanceChanged -= SaveBalance;
    }

    private void SaveBalance(int money)
    {
        YandexGame.savesData.money = money;
        _saver.TrySave();
    }
}