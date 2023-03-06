using System;
using System.Collections.Generic;
using CodeBase.UnitsSystem.StaticData;

namespace CodeBase.StaticData
{
    public interface IPlayerStats
    {
        int Gold { get; }
        Dictionary<UnitType, int> UnitsCount { get; }
        event Action OnUnitStatsChanged;
        void AddUnit(Unit unit);
    }
}