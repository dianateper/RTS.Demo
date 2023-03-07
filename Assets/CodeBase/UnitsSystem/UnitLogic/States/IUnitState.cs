namespace CodeBase.UnitsSystem.UnitLogic.States
{
    public interface IUnitState
    {
        UnitState StateId { get; }
        void Enter();
        void Update();
        void Exit();
    }
}