using System;
using Settings;
using UnityEngine;
using YG;
using Zenject;

namespace Misc
{
    public class DeviceDependentObject : MonoYandex
    {
        [Inject] 
        private GlobalSettings _globalSettings;
        private bool _isMobile = false;
        
        private enum DeviceType
        {
            OnlyMobile,
            OnlyDesktop
        }

        [SerializeField] private DeviceType _device;

        private void Start()
        {
            if (_globalSettings.ForceMobile) _isMobile = true;
        }

        protected override void OnSDK()
        {
            _isMobile = _globalSettings.ForceMobile || YandexGame.EnvironmentData.isMobile;
            DestroyIfWrongDevice();
        }

        private void DestroyIfWrongDevice()
        {
            if (_isMobile && _device == DeviceType.OnlyDesktop ||
                !_isMobile && _device == DeviceType.OnlyMobile)
            {
                Destroy(gameObject);
            }
        }
    }
}