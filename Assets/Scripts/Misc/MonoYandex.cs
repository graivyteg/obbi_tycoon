using System;
using UnityEngine;
using YG;

public class MonoYandex : MonoBehaviour
{
    private static YandexGame _yandex => YandexGame.Instance;
    
    protected virtual void OnEnable()
    {
        if (YandexGame.SDKEnabled) OnSDK();
        else YandexGame.GetDataEvent += OnSDK;
    }

    protected virtual void OnDisable()
    {
        YandexGame.GetDataEvent -= OnSDK;
    }

    protected virtual void OnSDK()
    {
        
    }
}
