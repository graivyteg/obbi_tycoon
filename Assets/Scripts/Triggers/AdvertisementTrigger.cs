using System;
using Audio;
using NaughtyAttributes;
using UnityEngine;
using YG;
using Zenject;

namespace Triggers
{
    public class AdvertisementTrigger : BasePlayerTrigger
    {
        [Inject] private MoneyGenerator _moneyGenerator;
        [Inject] private SoundController _soundController;
        [Inject] private Player _player;

        [SerializeField] private int _rewardMultiplier = 30;
        [SerializeField] private int _advertisementId;
        [Foldout("Advanced")]
        [SerializeField] private float _delay = 0.3f;
        
        private bool _isPlayerInside = false;
        private bool _adIsShown = false;
        private float _timer = 0;

        private void Start()
        {
            YandexGame.RewardVideoEvent += OnReward;
        }

        private void Update()
        {
            if (!_isPlayerInside)
            {
                _timer = 0;
                return;
            }

            _timer += Time.deltaTime;
            if (_timer < _delay || _adIsShown) return;
            
            YandexGame.RewVideoShow(_advertisementId);
            _adIsShown = true;
        }

        private void OnReward(int id)
        {
            if (id != _advertisementId) return;
            
            _player.Wallet.AddMoney(GetRewardAmount());
            _soundController.PlaySound(SoundController.SoundType.Cash);
        }

        protected override void OnPlayerEnter()
        {
            _isPlayerInside = true;
        }

        protected override void OnPlayerExit()
        {
            _adIsShown = false;
            _isPlayerInside = false;
        }
        
        public int GetRewardAmount()
        {
            return Mathf.RoundToInt(_moneyGenerator.MoneyPerSecond) * _rewardMultiplier;
        }
    }
}