using System;
using UnityEngine;
using Zenject;

namespace Settings
{
    public class GlobalSettingsInstaller : MonoInstaller
    {
        [SerializeField] private GlobalSettings _globalSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<GlobalSettings>().FromInstance(_globalSettings).AsSingle();
        }
    }
}