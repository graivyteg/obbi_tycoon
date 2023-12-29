using Audio;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Player _player;
    [SerializeField] private SoundController _soundController;
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameSaver>().FromNew().AsSingle();
        Container.Bind<Camera>().FromInstance(_mainCamera);
        Container.Bind<Player>().FromInstance(_player);
        Container.BindInterfacesAndSelfTo<MoneyGenerator>().AsSingle().NonLazy();
        Container.Bind<SoundController>().FromInstance(_soundController);
    }
}
