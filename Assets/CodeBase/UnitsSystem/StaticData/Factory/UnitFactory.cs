using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UnitsSystem.UnitLogic;
using UnityEngine;
using Zenject;

namespace CodeBase.UnitsSystem.StaticData.Factory
{
    [CreateAssetMenu(menuName = "RTS/Create UnitsData Factory", fileName = "UnitsDataFactory")]
    public class UnitFactory : ScriptableObject, IUnitFactory
    {
        [SerializeField] private UnitsData _unitsData;
        [SerializeField] private UnitSettings _unitSettings;

        private IInputService _inputService;
        private IPlayerStats _playerStats;
        private DiContainer _diContainer;
        
        [Inject]
        public void Construct(IInputService inputService, IPlayerStats playerStats, DiContainer diContainer)
        {
            _inputService = inputService;
            _playerStats = playerStats;
            _diContainer = diContainer;
        }
        
        public BaseWorldUnit CreateUnit(string unitId)
        {
            Unit unitData = _unitsData.GetUnit(unitId);
            BaseWorldUnit unit = _diContainer.InstantiatePrefab(unitData.UnitPrefab).GetComponent<BaseWorldUnit>();
            unit.Construct(unitData, _unitSettings, _inputService, _playerStats);
            return unit;
        }
    }
}
