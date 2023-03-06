using CodeBase.UnitsSystem.UnitLogic;

namespace CodeBase.UnitsSystem.StaticData.Factory
{
    public interface IUnitFactory
    {
        WorldUnit CreateBuilding(string unitId);
    }
}