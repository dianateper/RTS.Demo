using CodeBase.Services;
using CodeBase.UnitsSystem.UnitLogic;
using UnityEngine;
using Zenject;

namespace CodeBase.UnitsSystem
{
    public class UnitCommander : MonoBehaviour
    {
        private IInputService _inputService;
        private Camera _mainCamera;
        
        private const float Distance = 100;
        private const int BuildingLayer = 1 << 7;

        [Inject]
        public void Construct(IInputService inputService, Camera mainCamera)
        {
            _inputService = inputService;
            _mainCamera = mainCamera;

            _inputService.OnUnitSelect += SelectUnit;
        }

        private void OnDestroy()
        {
            _inputService.OnUnitSelect -= SelectUnit;
        }

        private void SelectUnit()
        {
            if (IsWorldUnitSelected(out var unit)) 
                unit.Action();
        }
        
        private bool IsWorldUnitSelected(out BaseWorldUnit unit)
        {
            unit = null;
            return Physics.Raycast(_mainCamera.ScreenPointToRay(_inputService.PointerPosition()), out RaycastHit hit,
                       Distance, BuildingLayer)
                   && hit.collider.TryGetComponent(out unit);
        }
    }
}