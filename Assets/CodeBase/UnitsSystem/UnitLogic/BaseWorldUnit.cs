using System;
using CodeBase.PlayerLogic;
using CodeBase.Services;
using CodeBase.UnitsSystem.StaticData;
using CodeBase.UnitsSystem.UnitLogic.States;
using TMPro;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public abstract class BaseWorldUnit : MonoBehaviour
    {
        [SerializeField] private UnitRenderer _unitOutlieRenderer;
        [SerializeField] private TMP_Text _stateText;
        [SerializeField] protected ProgressRenderer _progressRenderer;
        private IUnitState _currentUnitState;
        private IInputService _inputService;
        private UnitSettings _unitSettings;
        private UnitStateFactory _unitStateFactory;
        private PlaceUnitState _placeUnitState;
        private UnitBuildState _unitBuildUnitState;
        protected Unit _unit;
        protected IPlayerBase _playerBase;

        public UnitRenderer UnitRenderer => _unitOutlieRenderer;
        public IInputService InputService => _inputService;
        public UnitSettings UnitSettings => _unitSettings;
        public Unit Unit => _unit;
        public ProgressRenderer ProgressRender => _progressRenderer;
        
        public event Action OnUnitPlace;
        
        public void Construct(Unit unit, UnitSettings unitSettings, IInputService inputService, IPlayerBase playerBase)
        {
            _unitSettings = unitSettings;
            _inputService = inputService;
            _playerBase = playerBase;
            _unit = unit;
            _unitOutlieRenderer.Construct(_unitSettings);
            _unitStateFactory = new UnitStateFactory(this);
            
            CreateUnitPlaceState();
            CreateUnitBuildState();
            CreateAndEnterUnitIdleState();
        }

        private void Update()
        {
            _currentUnitState.Update();
        }

        private void OnDestroy()
        {
            _unitBuildUnitState.OnUnitBuild -= OnUnitUnitBuild;
            _currentUnitState?.Exit();
        }

        public abstract void DoAction();

        public void Action()
        {
            if (_currentUnitState.StateId == UnitState.Idle)
               ChangeState(UnitState.Action);
        }

        public void Select()
        {
            if (_currentUnitState.StateId == UnitState.Build) return;
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
        
        private void CreateAndEnterUnitIdleState()
        {
            _currentUnitState = _unitStateFactory.GetUnitState(UnitState.Idle);
            _currentUnitState.Enter();
        }

        private void CreateUnitBuildState()
        {
            _unitBuildUnitState = _unitStateFactory.GetUnitState(UnitState.Build) as UnitBuildState;
            _unitBuildUnitState.OnUnitBuild += OnUnitUnitBuild;
        }

        private void CreateUnitPlaceState()
        {
            _placeUnitState = _unitStateFactory.GetUnitState(UnitState.Place) as PlaceUnitState;
        }

        private void OnUnitUnitBuild()
        {
            _playerBase.PlayerStats.AddResource(_unit);
        }

        private void OnPlaceUnit()
        {
            _playerBase.PlayerStats.AddUnit(_unit);
            _placeUnitState.OnUnitPlaced -= OnPlaceUnit;
            OnUnitPlace?.Invoke();
        }
    }
}
