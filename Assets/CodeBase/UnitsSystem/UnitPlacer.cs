using CodeBase.PlayerLogic;
using CodeBase.Services;
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
        private BaseWorldUnit _selectedBaseWorldUnit;
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
            if (_selectedBaseWorldUnit != null) 
                DestroySelectedUnit();
          
            _selectedBaseWorldUnit = _unitFactory.CreateUnit(unit.UnitId);
            _selectedBaseWorldUnit.OnUnitPlace += ResetUnit;
            _selectedBaseWorldUnit.Select();
        }

        private void DestroyAndResetSelectedUnit()
        {
            if (_selectedBaseWorldUnit == null) return;
            _selectedBaseWorldUnit.OnUnitPlace -= ResetUnit;
            
            DestroySelectedUnit();

            _selectedBaseWorldUnit = null;
        }

        private void ResetUnit()
        {
            _selectedBaseWorldUnit.OnUnitPlace -= ResetUnit;
            _selectedBaseWorldUnit = null;
        }

        private void DestroySelectedUnit() => Destroy(_selectedBaseWorldUnit.gameObject);
    }
}
