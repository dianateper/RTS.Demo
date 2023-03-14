using CodeBase.UnitsSystem.UnitLogic;
using UnityEngine;

namespace CodeBase.UnitsSystem.StaticData.Factory
{
    public interface IUnitFactory
    {
        BaseWorldUnit CreateUnit(Vector3 at, string unitId);
    }
}