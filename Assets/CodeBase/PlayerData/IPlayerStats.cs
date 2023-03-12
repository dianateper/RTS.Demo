using System;
using System.Collections.Generic;
using CodeBase.UnitsSystem.StaticData;

namespace CodeBase.StaticData
{
    public interface IPlayerStats
    {
        int Gold { get; }
        float AttackPercent { get; }
        int Attack { get; }
        int Defense { get; }
        float DefensePercent { get; }
        Dictionary<UnitType, int> UnitsCount { get; }
        event Action OnUnitStatsChanged;
        event Action OnResourceChanged;
        void AddUnit(Unit unit);
        void AddResource(Unit unit);
    }
}