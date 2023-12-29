using System;
using UnityEngine;
using YG;

public class CitizensController : MonoYandex
{
    [SerializeField] private GameObject _citizensParent;

    private bool _citizensOn;
    
    protected override void OnSDK()
    {
        _citizensParent.SetActive(YandexGame.savesData.areCitizensOn);
        _citizensOn = YandexGame.savesData.areCitizensOn;
    }

    private void Update()
    {
        if (_citizensOn != YandexGame.savesData.areCitizensOn)
        {
            _citizensOn = YandexGame.savesData.areCitizensOn;
            _citizensParent.SetActive(_citizensOn);
        }

    }
}
