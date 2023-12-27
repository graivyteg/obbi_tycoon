using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Player _player;
    public override void InstallBindings()
    {
        Container.Bind<Camera>().FromInstance(_mainCamera);
        Container.Bind<Player>().FromInstance(_player);
        Container.BindInterfacesAndSelfTo<MoneyGenerator>().AsSingle().NonLazy();
    }
}
