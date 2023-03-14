using System;
using CodeBase.PlayerLogic;
using CodeBase.Services;
using CodeBase.UnitsSystem.StaticData;
using CodeBase.UnitsSystem.UnitLogic.States;
using Photon.Pun;
using TMPro;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public abstract class BaseWorldUnit : MonoBehaviour, IPunObservable
    {
        [SerializeField] private UnitRenderer _unitOutlieRenderer;
        [SerializeField] private TMP_Text _stateText;
        [SerializeField] protected ProgressRenderer _progressRenderer;
        [SerializeField] private PhotonView _photonView;
        private IUnitState _currentUnitState;
        private IInputService _inputService;
        private UnitSettings _unitSettings;
        private UnitStateFactory _unitStateFactory;
        private PlaceUnitState _placeUnitState;
        private UnitBuildState _unitBuildUnitState;
        protected Unit _unit;
        protected IPlayerBase _playerBase;
        private Vector3 _targetPosition;
        private Quaternion _targetRotation;
        private float _speed = 5;

        public UnitRenderer UnitRenderer => _unitOutlieRenderer;
        public IInputService InputService => _inputService;
        public UnitSettings UnitSettings => _unitSettings;
        public Unit Unit => _unit;
        public ProgressRenderer ProgressRender => _progressRenderer;
        public event Action OnUnitPlace;
        
        public void Construct(Unit unit, UnitSettings unitSettings, IInputService inputService, IPlayerBase playerBase)
        {
            if (_photonView.IsMine == false) return;
            _unitSettings = unitSettings;
            _inputService = inputService;
            _playerBase = playerBase;
            _unit = unit;
            _unitStateFactory = new UnitStateFactory(this);
            _unitOutlieRenderer.Construct(_unitSettings);
            CreateStatesAndEnterIdleState();
        }
        
        private void Update()
        {
            if (_photonView.IsMine)
            {
                _currentUnitState.Update();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime * _speed);
                transform.rotation =  Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);;
            }
        }

        private void OnDestroy()
        {
            if (_photonView.IsMine == false) return;
            _unitBuildUnitState.OnUnitBuild -= OnUnitUnitBuild;
            _currentUnitState?.Exit();
        }

        public abstract void DoAction();

        public void Action()
        {
            if (_photonView.IsMine == false) return;
            if (_currentUnitState.StateId == UnitState.Idle)
               ChangeState(UnitState.Action);
        }

        public void Select()
        {
            if (_photonView.IsMine == false) return;
            if (_currentUnitState.StateId == UnitState.Build) return;
            _placeUnitState.OnUnitPlaced += OnPlaceUnit;
            ChangeState(UnitState.Place);
        }

        public void ChangeState(UnitState stateId)
        {
            if (_photonView.IsMine == false) return;
            _currentUnitState?.Exit();
            var state = _unitStateFactory.GetUnitState(stateId);
            state.Enter();
            _currentUnitState = state;
            _stateText.text = $"{stateId}";
        }

        private void CreateStatesAndEnterIdleState()
        {
            if (_photonView.IsMine == false) return;
            _placeUnitState = _unitStateFactory.GetUnitState(UnitState.Place) as PlaceUnitState;
            _unitBuildUnitState = _unitStateFactory.GetUnitState(UnitState.Build) as UnitBuildState;
            _currentUnitState = _unitStateFactory.GetUnitState(UnitState.Idle);
            _unitBuildUnitState.OnUnitBuild += OnUnitUnitBuild;
            _currentUnitState.Enter();
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

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else
            {
                _targetPosition = (Vector3)stream.ReceiveNext();
                _targetRotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}
