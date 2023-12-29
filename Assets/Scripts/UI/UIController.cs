using System;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using YG;

namespace UI
{
    public class UIController : MonoYandex
    {
        [SerializeField] private CanvasGroup _settings;
        
        [Space(20)]
        [SerializeField] private Switcher _soundSwitcher;
        [SerializeField] private Switcher _musicSwitcher;
        [SerializeField] private Switcher _peopleSwitcher;
        [SerializeField] private Switcher _daytimeSwitcher;

        [Foldout("Advanced")] 
        [SerializeField] private float _fadeTime = 0.2f;

        private void Start()
        {
            _settings.alpha = 0;
            _settings.blocksRaycasts = false;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                OpenMenu();
            }
        }

        protected override void OnSDK()
        {
            _soundSwitcher.Switch(YandexGame.savesData.isSoundOn, false);
            _musicSwitcher.Switch(YandexGame.savesData.isMusicOn, false);
            _peopleSwitcher.Switch(YandexGame.savesData.areCitizensOn, false);
            _daytimeSwitcher.Switch(YandexGame.savesData.isDaytimeOn, false);
        }

        public void OpenMenu()
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            _settings.DOFade(1, _fadeTime).SetUpdate(true);
            _settings.blocksRaycasts = true;
        }

        public void SaveAndQuit()
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            _settings.blocksRaycasts = false;
            _settings.DOFade(0, _fadeTime).SetUpdate(true);
            
            YandexGame.savesData.isSoundOn = _soundSwitcher.IsOn;
            YandexGame.savesData.isMusicOn = _musicSwitcher.IsOn;
            YandexGame.savesData.areCitizensOn = _peopleSwitcher.IsOn;
            YandexGame.savesData.isDaytimeOn = _daytimeSwitcher.IsOn;
            YandexGame.SaveProgress();
        }
    }
}