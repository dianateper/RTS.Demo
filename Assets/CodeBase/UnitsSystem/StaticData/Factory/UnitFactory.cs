using CodeBase.PlayerLogic;
using CodeBase.Services;
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
        private IPlayerBase _playerBase;
        private DiContainer _diContainer;
        
        [Inject]
        public void Construct(IInputService inputService, IPlayerBase playerBase, DiContainer diContainer)
        {
            _inputService = inputService;
            _playerBase = playerBase;
            _diContainer = diContainer;
        }
        
        public BaseWorldUnit CreateUnit(string unitId)
        {
            Unit unitData = _unitsData.GetUnit(unitId);
            BaseWorldUnit unit = _diContainer.InstantiatePrefab(unitData.UnitPrefab).GetComponent<BaseWorldUnit>();
            unit.Construct(unitData, _unitSettings, _inputService, _playerBase);
            return unit;
        }
    }
}
