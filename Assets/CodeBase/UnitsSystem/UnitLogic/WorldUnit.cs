using System;
using System.Collections.Generic;
using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UnitsSystem.StaticData;
using CodeBase.UnitsSystem.UnitLogic.States;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public class WorldUnit : MonoBehaviour
    {
        [SerializeField] private UnitRenderer _unitOutlieRenderer;
        private IUnitState _currentUnitState;
        private IInputService _inputService;
        private UnitSettings _unitSettings;
        public UnitRenderer UnitRenderer => _unitOutlieRenderer;
        public IInputService InputService => _inputService;
        public UnitSettings UnitSettings => _unitSettings;
        public bool IsWorldUnit => _isWorldUnit;
        private IPlayerStats _playerStats;

        public event Action OnUnitPlace;

        private List<IUnitState> _unitStates = new List<IUnitState>();
        
        private PlaceUnitState _placeUnitState;
        private IdleUnitState _idleUnitState;
        private ProduceState _produceState;
        private bool _isWorldUnit;
        private Unit _unit;

        public void Construct(Unit unit, UnitSettings unitSettings, IInputService inputService,IPlayerStats playerStats)
        {
            _unitSettings = unitSettings;
            _inputService = inputService;
            _playerStats = playerStats;
            _unit = unit;
            _unitOutlieRenderer.Construct(_unitSettings);
            _placeUnitState = new PlaceUnitState(this);
            _idleUnitState = new IdleUnitState();
            _produceState = new ProduceState();
            _unitStates.AddRange(new IUnitState[]{_placeUnitState, _idleUnitState, _produceState});
            
            ChangeState<IdleUnitState>();
        }

        private void Update()
        {
            _currentUnitState.Update();
        }

        private void OnDestroy()
        {
            _currentUnitState?.Exit();
        }

        public void Select()
        {
            _placeUnitState.OnUnitPlaced += OnPlaceUnit;
            ChangeState<PlaceUnitState>();
        }

        public void ChangeState<T>()
        {
            _currentUnitState?.Exit();
            var state = _unitStates.Find(s => s.GetType() == typeof(T));
            state.Enter();
            _currentUnitState = state;
        }

        private void OnPlaceUnit()
        {
            if (_isWorldUnit == false)
            {
                _isWorldUnit = true;
                _playerStats.AddUnit(_unit);
            }

            _placeUnitState.OnUnitPlaced -= OnPlaceUnit;
            OnUnitPlace?.Invoke();
        }

        public void CancelPlace()
        {
            if (_isWorldUnit) 
                transform.position = _placeUnitState.OriginalPosition;
            
            ChangeState<ProduceState>();
        }
    }
}
