using System;
using System.Collections.Generic;
using CodeBase.StaticData;
using CodeBase.UnitsSystem.StaticData;
using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CodeBase.PlayerData
{
    [Serializable]
    public class PlayerStats : IPlayerStats
    {
        private int _gold;
        private int _attack;
        private int _defense;
        
        private Dictionary<UnitType, int> _unitsCount;
        private readonly int _maxAttack;
        private readonly int _maxDefense;
        private readonly Hashtable _props;

        public int Gold
        {
            get => _gold;

            set
            {
                OnGoldChanged?.Invoke();
                _gold = value;
            }
        }

        public float AttackPercent => (float)_attack / _maxAttack;
        public int Attack
        {
            get => _attack;
            set => _attack = value;
        }
        
        public int Defense
        {
            get => _defense;
            set => _defense = value;
        }

        public float DefensePercent => (float)_defense / _maxDefense;

        public Dictionary<UnitType, int> UnitsCount => _unitsCount;
        public event Action OnUnitsChanged;
        public event Action OnGoldChanged;
        
        [Inject]
        public PlayerStats(PlayerSettings playerSettings)
        {
            Gold = playerSettings.StartGold;
            _maxAttack = playerSettings.MaxAttack;
            _maxDefense = playerSettings.MaxDefense;
            _unitsCount = new Dictionary<UnitType, int>();
            _props = new Hashtable();
            UpdatePlayerCustomProperties();
        }

        public void AddUnit(Unit unit)
        {
            Gold -= unit.Cost;
            if (_unitsCount.ContainsKey(unit.UnitType))
                _unitsCount[unit.UnitType]++;
            else
                _unitsCount.Add(unit.UnitType, 1);
            
            switch (unit.UnitType)
            {
                case UnitType.Defense:
                    _defense = Mathf.Clamp(unit.Impact + _defense, 0, _maxDefense);
                    break;
                case UnitType.Attack:
                    _attack = Mathf.Clamp(unit.Impact + _attack, 0, _maxAttack);
                    break;
            }
            
            OnUnitsChanged?.Invoke();
            UpdatePlayerCustomProperties();
        }

        public void AddResource(Unit unit)
        {
            switch (unit.UnitType)
            {
                case UnitType.Resource:
                    Gold += unit.Impact;
                    break;
            }
        }

        private void UpdatePlayerCustomProperties()
        {
            _props[Constants.AttackKey] = Attack;
            _props[Constants.DefenseKey] = Defense;
            PhotonNetwork.LocalPlayer.SetCustomProperties(_props);
        }
    }
}