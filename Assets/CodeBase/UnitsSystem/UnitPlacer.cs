using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UI;
using CodeBase.UnitsSystem.StaticData;
using CodeBase.UnitsSystem.StaticData.Factory;
using CodeBase.UnitsSystem.UnitLogic;
using UnityEngine;
using Zenject;

namespace CodeBase.UnitsSystem
{
    public class UnitPlacer : MonoBehaviour
    {
        private IUnitFactory _unitFactory;
        private UnitViews _unitView;
        private WorldUnit _selectedWorldUnit;
        private IInputService _inputService;
        
        [Inject]
        public void Construct(IInputService inputService, UnitViews unitViews, IUnitFactory unitFactory, IPlayerStats playerStats, Camera mainCamera)
        {
            _unitView = unitViews;
            _inputService = inputService;
            _unitFactory = unitFactory;
        }

        private void OnEnable()
        {
            _unitView.OnUnitSelect += CreateAndSelectUnit;
            _inputService.OnCancel += DestroyAndResetSelectedUnit;
        }

        private void OnDisable()
        {
            _unitView.OnUnitSelect -= CreateAndSelectUnit;
            _inputService.OnCancel -= DestroyAndResetSelectedUnit;
        }

        private void CreateAndSelectUnit(Unit unit)
        {
            if (_selectedWorldUnit != null) 
                DestroySelectedUnit();
          
            _selectedWorldUnit = _unitFactory.CreateUnit(unit.UnitId);
            _selectedWorldUnit.OnUnitPlace += ResetUnit;
            _selectedWorldUnit.Select();
        }

        private void DestroyAndResetSelectedUnit()
        {
            if (_selectedWorldUnit == null) return;
            _selectedWorldUnit.OnUnitPlace -= ResetUnit;
            
            DestroySelectedUnit();

            _selectedWorldUnit = null;
        }

        private void ResetUnit()
        {
            _selectedWorldUnit.OnUnitPlace -= ResetUnit;
            _selectedWorldUnit = null;
        }

        private void DestroySelectedUnit() => Destroy(_selectedWorldUnit.gameObject);
    }
}
