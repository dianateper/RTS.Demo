using System;
using CodeBase.UnitsSystem.StaticData;
using UnityEngine;

namespace CodeBase.UI
{
    public class UnitViews : MonoBehaviour
    {
        private SelectUnitListView[] _unitViews;

        public event Action<Unit> OnUnitSelect;
        
        private void Awake()
        {
            _unitViews = GetComponentsInChildren<SelectUnitListView>();
        }

        private void OnEnable()
        {
            foreach (var unitListView in _unitViews) 
                unitListView.OnUnitSelect += SelectUnit;
        }

        private void OnDisable()
        {
            foreach (var unitListView in _unitViews) 
                unitListView.OnUnitSelect -= SelectUnit;
        }

        private void SelectUnit(Unit unit)
        {
            OnUnitSelect?.Invoke(unit);
        }
    }
}