using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using YG;

[RequireComponent(typeof(CanvasGroup))]
public class Tutorial : MonoYandex
{
    [SerializeField] private Button _button;
    
    private CanvasGroup _group;

    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
        _button.onClick.AddListener(Close);
    }

    protected override void OnSDK()
    {
        if (YandexGame.savesData.tutorialCompleted)
        {
            Close();
        }
        else
        {
            Observable.Timer(TimeSpan.FromSeconds(0.5)).Subscribe(_ =>
            {
                _group.DOFade(1, 0.3f).SetUpdate(true);
                _group.blocksRaycasts = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            });
        }
    }

    private void Close()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        YandexGame.savesData.tutorialCompleted = true;
        YandexGame.SaveProgress();

        _group.DOFade(0, 0.3f).SetUpdate(true);
        _group.blocksRaycasts = false;
    }
}