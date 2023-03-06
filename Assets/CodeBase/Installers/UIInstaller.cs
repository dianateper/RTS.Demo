using CodeBase.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UnitViews _unitViews;
        
        public override void InstallBindings()
        {
            Container.Bind<UnitViews>().FromComponentInNewPrefab(_unitViews).AsSingle()
                .NonLazy();
        }
    }
}
