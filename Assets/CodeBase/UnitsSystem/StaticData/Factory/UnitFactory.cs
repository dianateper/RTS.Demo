using CodeBase.Common;
using CodeBase.PlayerLogic;
using CodeBase.Services;
using CodeBase.UnitsSystem.UnitLogic;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CodeBase.UnitsSystem.StaticData.Factory
{
    [CreateAssetMenu(menuName = "RTS/Create UnitsData Factory", fileName = "UnitsDataFactory")]
    public class UnitFactory : ScriptableObject, IUnitFactory
    {
        [SerializeField] private UnitsData _unitsData;
        [SerializeField] private UnitSettings _unitSettings;

        private IInputService _inputService;
        private IPlayerBase _playerBase;
        private Transform _targetCamera;

        [Inject]
        public void Construct(IInputService inputService, IPlayerBase playerBase, Camera mainCamera)
        {
            _inputService = inputService;
            _playerBase = playerBase;
            _targetCamera = mainCamera.transform;
        }
        
        public BaseWorldUnit CreateUnit(Vector3 at, string unitId)
        {
            Unit unitData = _unitsData.GetUnit(unitId);
            BaseWorldUnit unit = PhotonNetwork.Instantiate(unitData.UnitPrefab.name, at, Quaternion.identity).GetComponent<BaseWorldUnit>();
            var unitView = unit.GetComponent<PhotonView>();
            if (unitView.IsMine)
                unit.GetComponentInChildren<Canvas>()?.gameObject.AddComponent<RotateTowardsCamera>()
                    .SetTarget(_targetCamera);
            unit.Construct(unitData, _unitSettings, _inputService, _playerBase);
            return unit;
        }
    }
}
