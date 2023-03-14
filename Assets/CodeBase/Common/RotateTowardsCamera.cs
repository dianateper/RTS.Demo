using UnityEngine;

namespace CodeBase.Common
{
    public class RotateTowardsCamera : MonoBehaviour
    {
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }
        
        private void LateUpdate()
        {
            if (_target == null) return;
            transform.LookAt(transform.position + _target.rotation * Vector3.forward,
                _target.rotation * Vector3.up);
        }
    }
}
