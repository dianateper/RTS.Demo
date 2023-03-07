using UnityEngine;
using Zenject;

namespace CodeBase.Common
{
    public class RotateTowardsCamera : MonoBehaviour
    {
        private Transform _target;

        [Inject]
        public void Construct(Camera mainCamera)
        {
            _target = mainCamera.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _target.rotation * Vector3.forward,
                _target.rotation * Vector3.up);
        }
    }
}
