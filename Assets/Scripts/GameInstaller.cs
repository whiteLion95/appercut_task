using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private Player _player;

    public override void InstallBindings()
    {
        Container.Bind<Player>().FromInstance(_player).AsSingle().NonLazy();
    }
}