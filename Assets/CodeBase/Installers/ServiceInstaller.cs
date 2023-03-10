using CodeBase.PlayerLogic;
using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UnitsSystem.StaticData;
using CodeBase.UnitsSystem.StaticData.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        [SerializeField] private InputService _inputService;
     
        public override void InstallBindings()
        {
            Container.Bind<Camera>().FromInstance(Camera.main).AsSingle().NonLazy();
            Container.Bind<IInputService>().To<InputService>().FromComponentInNewPrefab(_inputService).AsSingle().NonLazy();
            Container.Bind<IPlayerBase>().To<PlayerBase>().AsSingle().NonLazy();
            Container.Bind<PlayerFactory>().FromScriptableObjectResource(Constants.PlayerFactoryPath).AsSingle().NonLazy();
            Container.Bind<IUnitFactory>().To<UnitFactory>().FromScriptableObjectResource(Constants.UnitFactoryPath).AsSingle().NonLazy();
            Container.Bind<UnitsData>().FromScriptableObjectResource(Constants.UnitsDataPath).AsSingle().NonLazy();
            Container.Bind<LevelStaticData>().FromScriptableObjectResource(Constants.LevelStaticData).AsSingle().NonLazy();
        }
    }
}
