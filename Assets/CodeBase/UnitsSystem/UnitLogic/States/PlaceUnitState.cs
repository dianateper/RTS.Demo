using System;
using CodeBase.Services;
using CodeBase.UnitsSystem.StaticData;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic.States
{
    public class PlaceUnitState : IUnitState
    {
        private readonly WorldUnit _context;
        private IInputService _inputService;
        private UnitSettings _unitSettings;
        private UnitRenderer _unitOutlineRenderer;
        private Collider[] _validColliders;
        private Camera _mainCamera;
        private Vector3 _targetPosition;
        private const int BuildingLayer = 1 << 7;
        private const float Distance = 100;
        private const int GroundLayer = 1 << 6;

        public event Action OnUnitPlaced;

        private Vector3 _originalPosition;
        public Vector3 OriginalPosition => _originalPosition;
        public UnitState StateId => UnitState.Place;


        public PlaceUnitState(WorldUnit context)
        {
            _context = context;
        }

        public void Enter()
        {
            _unitOutlineRenderer = _context.UnitRenderer; 
            _inputService = _context.InputService; 
            _unitSettings = _context.UnitSettings; 
            _validColliders = new Collider[2];
            _mainCamera = Camera.main;
            _unitOutlineRenderer.EnableRenderer();
            _inputService.OnUnitSelect += PlaceUnit;
        }

        public void Exit()
        {
            _inputService.OnUnitSelect -= PlaceUnit;
            _unitOutlineRenderer.DisableRenderer();
        }

        public void Update()
        {
            if (HitGround(out var hit))
            {
                if (_inputService.UnitRotate(out var delta))
                    Rotate(delta);
                else
                    SetPosition(hit.point);

                SetValid(IsValidPlace());
            }

            MoveToTargetPosition();
        }

        private void MoveToTargetPosition()
        {
            if (_targetPosition != Vector3.zero)
                _context.transform.position = Vector3.Lerp(_context.transform.position, _targetPosition,
                    Time.deltaTime * _unitSettings.Speed);
        }

        private void PlaceUnit()
        {
            if (HitGround(out var hit) && IsValidPlace())
            {
                Place(hit.point);
                OnUnitPlaced?.Invoke();
                _context.ChangeState(UnitState.Produce);
            }
        }

        private void Rotate(float delta) => _context.transform.Rotate(Vector3.up, delta * _unitSettings.RotationAngle);

        private void SetPosition(Vector3 position) => _targetPosition = position;

        private void SetValid(bool isValid) => _unitOutlineRenderer.ChangeColor(isValid);

        private bool HitGround(out RaycastHit hit) => 
            Physics.Raycast(_mainCamera.ScreenPointToRay(_inputService.PointerPosition()), out hit, Distance, GroundLayer);
        
        private bool IsValidPlace() => 
            Physics.OverlapSphereNonAlloc(_context.transform.position, 1, _validColliders, 
                BuildingLayer) <= 1;
        
        private void Place(Vector3 position)
        {
            _context.transform.position = position;
            _originalPosition = position;
        }
    }
}