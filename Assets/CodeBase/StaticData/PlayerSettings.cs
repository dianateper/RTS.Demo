using System;
using UnityEngine;

namespace CodeBase.StaticData
{
    [Serializable]
    public class PlayerSettings
    {
        [SerializeField] private int _maxAttack;
        [SerializeField] private int _maxDefense;
        [SerializeField] private int _startGold;

        public int MaxAttack => _maxAttack;
        public int MaxDefense => _maxDefense;
        public int StartGold => _startGold;
    }
}