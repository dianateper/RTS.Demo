using System;
using System.Linq;
using UnityEngine;

namespace CodeBase.UnitsSystem.StaticData
{
    [CreateAssetMenu(menuName = "RTS/Create UnitsData", fileName = "UnitsData", order = 0)]
    [Serializable]
    public class UnitsData : ScriptableObject
    {
        [SerializeField] private Unit[] _units;
        public Unit GetUnit(string unitId) => _units.FirstOrDefault(u => u.UnitId == unitId);
        public Unit[] GetUnits(UnitType unitType) => _units.Where(u => u.UnitType == unitType).ToArray();
    }
}