using System;
using System.Collections.Generic;
using CodeBase.StaticData;
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
        private IPlayerStats _playerStats;
        
        public event Action<Unit> OnUnitSelect;

        [Inject]
        public void Construct(IPlayerStats playerStats, UnitsData unitsData)
        {
            _playerStats = playerStats;
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
                _unitsView.Add(unitView.Construct(unit, _playerStats));
            }
        }

        private void SelectUnit(Unit unit)
        {
            OnUnitSelect?.Invoke(unit);
        }
    }
}
