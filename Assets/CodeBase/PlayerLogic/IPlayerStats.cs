using System;
using System.Collections.Generic;
using CodeBase.UnitsSystem.StaticData;

namespace CodeBase.PlayerLogic
{
    public interface IPlayerStats
    {
        int Gold { get; }
        float AttackPercent { get; }
        int Attack { get; }
        int Defense { get; }
        float DefensePercent { get; }
        Dictionary<UnitType, int> UnitsCount { get; }
        event Action OnGoldChanged;
        event Action OnUnitsChanged;
        void AddUnit(Unit unit);
        void AddResource(Unit unit);
    }
}