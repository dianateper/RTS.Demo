using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic.States
{
    public class UnitBuildState : IUnitState
    {
        private readonly ProgressRenderer _progressRenderer;
        private readonly BaseWorldUnit _context;
        private float _time;
        private float _progress;
        private WaitForSeconds _delay;

        public event Action OnUnitBuild;
        
        public UnitState StateId => UnitState.Build;

        public UnitBuildState(BaseWorldUnit context)
        {
            _context = context;
            _progressRenderer = _context.ProgressRender;
            _delay = new WaitForSeconds(_context.Unit.ProductionRate);
        }

        public void Enter()
        {
            _context.StartCoroutine(Build());
            _progressRenderer.gameObject.SetActive(true);
            _progressRenderer.AnimateProgress(0, _context.Unit.ProductionRate);
        }

        public void Update()
        {
            
        }

        private IEnumerator Build()
        {
            yield return _delay;
            OnUnitBuild?.Invoke();
            _context.ChangeState(UnitState.Idle);
        }
        
        public void Exit()
        {
            _progressRenderer.gameObject.SetActive(false);
        }
    }
}