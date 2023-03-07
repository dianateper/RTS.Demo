namespace CodeBase.UnitsSystem.UnitLogic.States
{
    public class UnitActionState : IUnitState
    {
        public UnitState StateId => UnitState.Action;
        private readonly BaseWorldUnit _context;

        public UnitActionState(BaseWorldUnit context)
        {
            _context = context;
        }

        public void Enter()
        {
            _context.DoAction();
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}