using CodeBase.Network;
using Zenject;

namespace CodeBase.Installers
{
    public class NetworkInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NetworkLevelHandler>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<PlayersDashboard>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}