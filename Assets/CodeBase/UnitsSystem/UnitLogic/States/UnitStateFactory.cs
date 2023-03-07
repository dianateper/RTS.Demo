using System;
using System.Collections.Generic;

namespace CodeBase.UnitsSystem.UnitLogic.States
{
    public class UnitStateFactory
    {
        private readonly List<IUnitState> _unitStates;
        private readonly BaseWorldUnit _context;

        public UnitStateFactory(BaseWorldUnit context)
        {
            _unitStates = new List<IUnitState>();
            _context = context;
        }

        public IUnitState GetUnitState(UnitState stateId)
        {
            var state =  _unitStates.Find(s => s.StateId == stateId);
            if (state == null)
                state = CreateUnitState(stateId);
            return state;
        }

        private IUnitState CreateUnitState(UnitState unitState)
        {
            IUnitState state;
            switch (unitState)
            {
                case UnitState.Idle:
                    state = new IdleUnitState();
                    break;
                case UnitState.Place:
                    state = new PlaceUnitState(_context);
                    break;
                case UnitState.Build:
                    state = new UnitBuildState(_context);
                    break;
                case UnitState.Action:
                    state = new UnitActionState(_context);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unitState), unitState, null);
            }
            _unitStates.Add(state);
            return state;
        }
    }
}