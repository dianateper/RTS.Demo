using System;
using CodeBase.UnitsSystem.UnitLogic;
using UnityEngine;

namespace CodeBase.UnitsSystem.StaticData
{
    [CreateAssetMenu(menuName = "RTS/Create Unit", fileName = "Unit", order = 0)]
    [Serializable]
    public class Unit : ScriptableObject
    {
        [SerializeField] private UnitType _unitType;
        [SerializeField] private WorldUnit _unitPrefab;
        [SerializeField] private string _name;
        [SerializeField] private string _unitId;
        [SerializeField] private int _cost;
        [SerializeField] private Sprite _sprite;

        public string Name => _name;
        public int Cost => _cost;
        public Sprite Sprite => _sprite;
        public UnitType UnitType => _unitType;
        public WorldUnit UnitPrefab => _unitPrefab;
        public string UnitId => _unitId;
       
        [SerializeField, HideInInspector]
        private bool _initialized;
        
        private void OnEnable()
        {
            if (!_initialized)
            {
                _unitId = Guid.NewGuid().ToString();
                _initialized = true;
            }
        }
    }
}