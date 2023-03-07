using CodeBase.UnitsSystem.UnitLogic;

namespace CodeBase.UnitsSystem.StaticData.Factory
{
    public interface IUnitFactory
    {
        WorldUnit CreateUnit(string unitId);
    }
}