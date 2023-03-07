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
        private Camera _mainCamera;
        private IInputService _inputService;
       
        private const float Distance = 100;
        private const int BuildingLayer = 1 << 7;

        [Inject]
        public void Construct(IInputService inputService, UnitViews unitViews, IUnitFactory unitFactory, IPlayerStats playerStats, Camera mainCamera)
        {
            _unitView = unitViews;
            _inputService = inputService;
            _unitFactory = unitFactory;
            _mainCamera = mainCamera;
        }

        private void OnEnable()
        {
            _unitView.OnUnitSelect += CreateAndSelectUnit;
            _inputService.OnUnitSelect += SelectWorldUnit;
            _inputService.OnCancel += DestroyAndResetSelectedUnit;
        }

        private void OnDisable()
        {
            _unitView.OnUnitSelect -= CreateAndSelectUnit;
            _inputService.OnUnitSelect -= SelectWorldUnit;
            _inputService.OnCancel -= DestroyAndResetSelectedUnit;
        }

        private void SelectWorldUnit()
        {
            if (_selectedWorldUnit == null && IsWorldUnitSelected(out var unit))
            {
                _selectedWorldUnit = unit;
                _selectedWorldUnit.Select();
                _selectedWorldUnit.OnUnitPlace += ResetUnit;
            }
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
            
            if (_selectedWorldUnit.IsWorldUnit)
                _selectedWorldUnit.CancelPlace();
            else
                DestroySelectedUnit();

            _selectedWorldUnit = null;
        }

        private void ResetUnit()
        {
            _selectedWorldUnit.OnUnitPlace -= ResetUnit;
            _selectedWorldUnit = null;
        }

        private void DestroySelectedUnit() => Destroy(_selectedWorldUnit.gameObject);

        private bool IsWorldUnitSelected(out WorldUnit unit)
        {
            unit = null;
            return Physics.Raycast(_mainCamera.ScreenPointToRay(_inputService.PointerPosition()), out RaycastHit hit,
                       Distance, BuildingLayer)
                   && hit.collider.TryGetComponent(out unit);
        }
    }
}
