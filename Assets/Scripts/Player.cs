using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

public class Player : MonoYandex
{
    public Wallet Wallet = new Wallet();

    protected override void OnSDK()
    {
        base.OnSDK();
        Wallet.AddMoney(YandexGame.savesData.money[SceneManager.GetActiveScene().buildIndex]);
        Wallet.OnBalanceChanged += SaveBalance;
    }

    protected override void OnDisable()
    {
        Wallet.OnBalanceChanged -= SaveBalance;
    }

    private void SaveBalance(int value)
    {
        YandexGame.savesData.money[SceneManager.GetActiveScene().buildIndex] = value;
        YandexGame.SaveProgress();
    }
}