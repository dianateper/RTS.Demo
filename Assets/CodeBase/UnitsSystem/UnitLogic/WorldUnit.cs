using System;
using CodeBase.Services;
using CodeBase.StaticData;
using CodeBase.UnitsSystem.StaticData;
using CodeBase.UnitsSystem.UnitLogic.States;
using TMPro;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public class WorldUnit : MonoBehaviour
    {
        [SerializeField] private UnitRenderer _unitOutlieRenderer;
        [SerializeField] private TMP_Text _stateText;
        [SerializeField] private ProgressRenderer _progressRenderer;
        private IUnitState _currentUnitState;
        private IInputService _inputService;
        private UnitSettings _unitSettings;
        private IPlayerStats _playerStats;
        private Unit _unit;
        private UnitStateFactory _unitStateFactory;
        private PlaceUnitState _placeUnitState;

        private bool _isWorldUnit;

        public UnitRenderer UnitRenderer => _unitOutlieRenderer;
        public IInputService InputService => _inputService;
        public UnitSettings UnitSettings => _unitSettings;
        public bool IsWorldUnit => _isWorldUnit;
        public Unit Unit => _unit;
        public ProgressRenderer ProgressRate => _progressRenderer;
        public event Action OnUnitPlace;

        public void Construct(Unit unit, UnitSettings unitSettings, IInputService inputService,IPlayerStats playerStats)
        {
            _unitSettings = unitSettings;
            _inputService = inputService;
            _playerStats = playerStats;
            _unit = unit;
            _unitOutlieRenderer.Construct(_unitSettings);
            _unitStateFactory = new UnitStateFactory(this);
            _placeUnitState = _unitStateFactory.GetUnitState(UnitState.Place) as PlaceUnitState;
            _currentUnitState = _unitStateFactory.GetUnitState(UnitState.Idle);
            _currentUnitState.Enter();
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
            if (_currentUnitState.StateId == UnitState.Produce) return;
            _placeUnitState.OnUnitPlaced += OnPlaceUnit;
            ChangeState(UnitState.Place);
        }

        public void ChangeState(UnitState stateId)
        {
            _currentUnitState?.Exit();
            var state = _unitStateFactory.GetUnitState(stateId);
            state.Enter();
            _currentUnitState = state;
            _stateText.text = $"{stateId}";
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
           
            ChangeState(UnitState.Produce);
        }
    }
}
