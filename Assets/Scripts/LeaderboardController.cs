using System;
using UnityEngine;
using YG;
using Zenject;

public class LeaderboardController : MonoYandex
{
    [SerializeField] private string _leaderboardKey = "money";
    [SerializeField] private float _cooldown = 2;
    
    [Inject] private Player _player;
    private int _record;
    private float _timer;

    private void Start()
    {
        _timer = _cooldown;
    }

    protected override void OnSDK()
    {
        _record = YandexGame.savesData.Record;
    }
    
    private void Update()
    {
        _timer -= Mathf.Min(_timer, Time.deltaTime);
        if (_timer > 0) return;
        
        if (_player.Wallet.Money > _record)
        {
            Debug.Log("New Record!");
            YandexGame.NewLeaderboardScores(_leaderboardKey, _player.Wallet.Money);
            YandexGame.savesData.Record = _player.Wallet.Money;
            YandexGame.SaveProgress();
            _timer = _cooldown;
        }
    }
}