using CodeBase.Network;
using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UnitViews _unitViews;
        [SerializeField] private PopupSystem _popupSystem;
        [SerializeField] private Menu _menu;
      
        public override void InstallBindings()
        {
            Container.Bind<PopupSystem>().FromComponentInNewPrefab(_popupSystem).AsSingle().NonLazy();
            Container.Bind<UnitViews>().FromComponentInNewPrefab(_unitViews).AsSingle().NonLazy();

            var menu = Container.InstantiatePrefab(_menu).GetComponent<Menu>();
            Container.Bind<Menu>().FromInstance(menu).AsSingle().NonLazy();
            Container.Bind<PlayersDashboardView>()
                .FromComponentOn(menu.GetComponentInChildren<PlayersDashboardView>(true).gameObject).AsSingle().NonLazy();
        }
    }
}
