using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic.States
{
    public class ProduceState : IUnitState
    {
        private readonly ProgressRenderer _progressRenderer;
        private readonly WorldUnit _context;
        private readonly float _productionRate;

        private float _time;
        private float _progress;
        
        public UnitState StateId => UnitState.Produce;

        public ProduceState(WorldUnit context)
        {
            _context = context;
            _progressRenderer = _context.ProgressRate;
            _productionRate = _context.Unit.ProductionRate;
        }

        public void Enter()
        {
            _time = 0;
            _progress = 0;
            _progressRenderer.gameObject.SetActive(true);
        }

        public void Update()
        {
            _progress = (_time * 100f) / _productionRate;
            if (_progress >= 100) 
                _context.ChangeState(UnitState.Idle);

            _progressRenderer.UpdateProgress(_progress / 100f);
            _time += Time.deltaTime;
        }

        public void Exit()
        {
            _progressRenderer.gameObject.SetActive(false);
        }
    }
}