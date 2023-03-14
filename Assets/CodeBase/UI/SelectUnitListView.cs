using System;
using System.Collections.Generic;
using CodeBase.PlayerLogic;
using CodeBase.UnitsSystem.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class SelectUnitListView : MonoBehaviour
    {
        [SerializeField] private UnitType _unitType;
        [SerializeField] private SelectUnitView _selectUnitView;
        [SerializeField] private Transform _parent;
        
        private UnitsData _unitsData;
        private List<SelectUnitView> _unitsView;
        private IPlayerBase _playerBase;
        
        public event Action<Unit> OnUnitSelect;

        [Inject]
        public void Construct(IPlayerBase playerBase, UnitsData unitsData)
        {
            _playerBase = playerBase;
            _unitsData = unitsData;
        }
        
        private void Start() => LoadUnits();

        private void OnDestroy()
        {
            foreach (var unitView in _unitsView) 
                unitView.OnUnitSelect -= SelectUnit;
        }

        private void LoadUnits()
        {
            _unitsView = new List<SelectUnitView>();
            var units = _unitsData.GetUnits(_unitType);
            foreach (var unit in units)
            {
                var unitView = Instantiate(_selectUnitView, _parent);
                unitView.OnUnitSelect += SelectUnit;
                unitView.gameObject.SetActive(true);
                _unitsView.Add(unitView.Construct(unit, _playerBase));
            }
        }

        private void SelectUnit(Unit unit)
        {
            OnUnitSelect?.Invoke(unit);
        }
    }
}
