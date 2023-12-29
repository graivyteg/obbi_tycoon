using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI
{
    [RequireComponent(typeof(Slider))]
    public class SensitivitySlider : MonoYandex
    {
        private Slider _slider;

        protected override void OnSDK()
        {
            _slider = GetComponent<Slider>();
            _slider.value = YandexGame.savesData.sensitivityMultiplier;
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnValueChanged(float newValue)
        {
            YandexGame.savesData.sensitivityMultiplier = newValue;
        }
    }
}