using System;
using System.Collections.Generic;
using CodeBase.UnitsSystem.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.StaticData
{
    public class PlayerStats : IPlayerStats
    {
        private int _gold;
        private int _attack;
        private int _defense;
        
        private Dictionary<UnitType, int> _unitsCount;
        private readonly int _maxAttack;
        private readonly int _maxDefense;

        public int Gold => _gold;
        public float Attack => (float)_attack / _maxAttack;
        public float Defense => (float)_defense / _maxDefense;
        public Dictionary<UnitType, int> UnitsCount => _unitsCount;
        public event Action OnResourceChanged;
        public event Action OnUnitStatsChanged;
        
        [Inject]
        public PlayerStats(PlayerSettings playerSettings)
        {
            _gold = playerSettings.StartGold;
            _maxAttack = playerSettings.MaxAttack;
            _maxDefense = playerSettings.MaxDefense;
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

        public void AddResource(Unit unit)
        {
            switch (unit.UnitType)
            {
                case UnitType.Resource:
                    _gold += unit.Impact;
                    break;
                case UnitType.Defense:
                    _defense = Mathf.Clamp(unit.Impact + _defense, 0, _maxDefense);
                    break;
                case UnitType.Attack:
                    _attack = Mathf.Clamp(unit.Impact + _attack, 0, _maxAttack);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            OnResourceChanged?.Invoke();
        }
    }
}