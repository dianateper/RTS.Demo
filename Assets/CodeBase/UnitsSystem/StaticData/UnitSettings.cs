using System;
using UnityEngine;

namespace CodeBase.UnitsSystem.StaticData
{
    [Serializable]
    public class UnitSettings
    {
        [SerializeField] private Color _validColor;
        [SerializeField] private Color _inValidColor;
        [SerializeField] private float _rotationAngle = 15f;
        [SerializeField] private float _speed = 10;

        public Color ValidColor => _validColor;
        public Color InValidColor => _inValidColor;
        public float RotationAngle => _rotationAngle;
        public float Speed => _speed;
    }
}