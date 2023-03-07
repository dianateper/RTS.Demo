using System;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic.States
{
    public class UnitBuildState : IUnitState
    {
        private readonly ProgressRenderer _progressRenderer;
        private readonly BaseWorldUnit _context;
        private readonly float _productionRate;
        private float _time;
        private float _progress;

        public event Action OnUnitBuild;
        
        public UnitState StateId => UnitState.Build;

        public UnitBuildState(BaseWorldUnit context)
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
            _progress = _time * 100f / _productionRate;
            if (_progress >= 100)
            {
                OnUnitBuild?.Invoke();
                _context.ChangeState(UnitState.Idle);
            }

            _progressRenderer.UpdateProgress(_progress / 100f);
            _time += Time.deltaTime;
        }

        public void Exit()
        {
            _progressRenderer.gameObject.SetActive(false);
        }
    }
}