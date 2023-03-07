using CodeBase.UnitsSystem.UnitLogic;

namespace CodeBase.UnitsSystem.StaticData.Factory
{
    public interface IUnitFactory
    {
        BaseWorldUnit CreateUnit(string unitId);
    }
}