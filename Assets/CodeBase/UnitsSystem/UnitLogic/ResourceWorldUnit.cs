using System.Collections;
using CodeBase.UnitsSystem.UnitLogic.States;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public class ResourceWorldUnit : BaseWorldUnit
    {
        private WaitForSeconds _delay;
        
        public override void DoAction()
        {
            _delay = new WaitForSeconds(_unit.ProductionRate);
            _progressRenderer.gameObject.SetActive(true);
            StartCoroutine(ProduceResource());
        }

        private IEnumerator ProduceResource()
        {
            _progressRenderer.AnimateProgress(0, _unit.ProductionRate);
            yield return _delay;
            ChangeState(UnitState.Idle);
            _playerStats?.AddResource(_unit);
            _progressRenderer.gameObject.SetActive(false);
        }
    }
}