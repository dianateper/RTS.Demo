using CodeBase.UnitsSystem.UnitLogic;
using UnityEngine;

namespace CodeBase.UnitsSystem.StaticData.Factory
{
    [CreateAssetMenu(menuName = "RTS/Create UnitsData Factory", fileName = "UnitsDataFactory")]
    public class UnitFactory : ScriptableObject, IUnitFactory
    {
        [SerializeField] private UnitsData _unitsData;
        [SerializeField] private UnitSettings _unitSettings;

        public WorldUnit CreateBuilding(string unitId)
        {
            var unitData = _unitsData.GetUnit(unitId);
            var unit = Instantiate(unitData.UnitPrefab);
            unit.Construct(_unitSettings);
            return unit;
        }
    }
}
