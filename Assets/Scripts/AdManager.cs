using System;
using TMPro;
using UnityEngine;
using YG;

public class AdManager : MonoYandex
{
    [SerializeField] private float _cooldown = 75;
    [SerializeField] private TMP_Text _warningText;
    [SerializeField] private string _textFormat;

    private float _timer;

    private void Start()
    {
        _warningText.text = "";
        _timer = _cooldown;
    }

    private void Update()
    {
        _timer -= Mathf.Min(_timer, Time.deltaTime);

        if (_timer <= 5)
        {
            _warningText.gameObject.SetActive(true);
            _warningText.text = string.Format(_textFormat, Mathf.CeilToInt(_timer));
        }
        else
        {
            _warningText.text = "";
        }

        if (_timer == 0)
        {
            YandexGame.FullscreenShow();
            _timer = _cooldown;
        }
        
    }
}
