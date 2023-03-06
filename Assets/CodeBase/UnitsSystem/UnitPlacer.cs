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
        private Collider[] _validColliders;
        private WorldUnit _selectedWorldUnit;
        private Camera _mainCamera;
        private IInputService _inputService;

        private const float Distance = 100;
        private const int GroundLayer = 1 << 6;
        private const int BuildingLayer = 1 << 7;
        
        [Inject]
        public void Construct(IInputService inputService, UnitViews unitViews, IUnitFactory unitFactory)
        {
            _unitView = unitViews;
            _inputService = inputService;
            _unitFactory = unitFactory;
        }

        private void Awake()
        {
            _validColliders = new Collider[2];
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _unitView.OnUnitSelect += SelectUnit;
            _inputService.OnUnitSelect += SelectWorldUnit;
            _inputService.OnCancel += ResetSelectedUnit;
        }

        private void OnDisable()
        {
            _unitView.OnUnitSelect -= SelectUnit;
            _inputService.OnUnitSelect -= SelectWorldUnit;
            _inputService.OnCancel -= ResetSelectedUnit;
        }

        private void Update()
        {
            if(_selectedWorldUnit == null) 
                return;

            if (HitGround(out var hit))
            {
                if (_inputService.UnitRotate(out var delta))
                    _selectedWorldUnit.Rotate(delta);
                else
                    _selectedWorldUnit.SetPosition(hit.point);
                _selectedWorldUnit.SetValid(IsValidPlace());
            }
        }

        private void SelectWorldUnit()
        {
            if (_selectedWorldUnit == null && IsWorldUnitSelected(out var unit))
            {
                _selectedWorldUnit = unit;
                _selectedWorldUnit.Select();
                return;
            }
            
            if(_selectedWorldUnit == null) 
                return;
            
            if (HitGround(out var hit) && IsValidPlace())
            {
                PlaceUnit(hit.point);
                _selectedWorldUnit = null;
            }
        }

        private bool IsWorldUnitSelected(out WorldUnit unit)
        {
            unit = null;
            return Physics.Raycast(_mainCamera.ScreenPointToRay(_inputService.PointerPosition()), out RaycastHit hit,
                       Distance, BuildingLayer)
                   && hit.collider.TryGetComponent(out unit);
        }

        private void SelectUnit(Unit unit)
        {
            if (_selectedWorldUnit != null) 
                DestroySelectedUnit();
          
            _selectedWorldUnit = _unitFactory.CreateBuilding(unit.UnitId);
        }

        private void ResetSelectedUnit()
        {
            if (_selectedWorldUnit == null) return;
            DestroySelectedUnit();
        }

        private void DestroySelectedUnit()
        {
            Destroy(_selectedWorldUnit.gameObject);
            _selectedWorldUnit = null;
        }

        private bool IsValidPlace() => 
            Physics.OverlapSphereNonAlloc(_selectedWorldUnit.transform.position, 1, _validColliders, 
                BuildingLayer) <= 1;

        private void PlaceUnit(Vector3 position) => 
            _selectedWorldUnit.Place(position);

        private bool HitGround(out RaycastHit hit) => 
            Physics.Raycast(_mainCamera.ScreenPointToRay(_inputService.PointerPosition()), out hit, Distance, GroundLayer);
    }
}
