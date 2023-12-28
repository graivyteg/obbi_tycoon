using UnityEngine;
using YG;
using Zenject;

public class GameSaver : ITickable
{
    private YandexGame _yandex => YandexGame.Instance;
    
    private float _timer = 0;
    private const int _delay = 2;

    private bool _shouldSave = false;

    public void Tick()
    {
        if (_timer <= 0)
        {
            if (_shouldSave) TrySave();
            return;
        }
        _timer -= Time.deltaTime;
    }

    public void TrySave()
    {
        if (_timer > 0)
        {
            _shouldSave = true;
            return;
        }

        _shouldSave = false;
        _yandex._SaveProgress();
        Debug.Log("Game successfully saved");
    }
}
