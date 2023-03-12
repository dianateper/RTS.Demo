using CodeBase.Network;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UnitViews _unitViews;
        [SerializeField] private PlayersDashboardView _playersDashboard;
        
        public override void InstallBindings()
        {
            Container.Bind<UnitViews>().FromComponentInNewPrefab(_unitViews).AsSingle().NonLazy();
            Container.Bind<PlayersDashboardView>().FromComponentInNewPrefab(_playersDashboard).AsSingle().NonLazy();
        }
    }
}
