using System;
using System.Collections.Generic;
using CodeBase.UnitsSystem.StaticData;

namespace CodeBase.StaticData
{
    public class PlayerStats : IPlayerStats
    {
        private int _gold;
        private Dictionary<UnitType, int> _unitsCount;

        public int Gold => _gold;
        public Dictionary<UnitType, int> UnitsCount => _unitsCount;
        
        public PlayerStats()
        {
            _gold = 100;
            _unitsCount = new Dictionary<UnitType, int>();
        }

        public void AddUnit(Unit unit)
        {
            _gold -= unit.Cost;
            if (_unitsCount.ContainsKey(unit.UnitType))
                _unitsCount[unit.UnitType]++;
            else
                _unitsCount.Add(unit.UnitType, 1);
            OnUnitStatsChanged?.Invoke();
        }

        public event Action OnUnitStatsChanged;
    }
}