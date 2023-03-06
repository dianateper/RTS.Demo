using CodeBase.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.CameraLogic
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _speed = 5; 
        
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Update()
        {
            HandleZoom();
            HandleMovement();
        }
        
        private void HandleMovement()
        {
            var horizontal = _inputService.GetHorizontalMovement();
            var vertical = _inputService.GetVerticalMovement();

            var movement = new Vector3(horizontal, 0, vertical);
            transform.position += movement * (Time.deltaTime * _speed);
        }

        private void HandleZoom()
        {
            _camera.fieldOfView -= _inputService.GetZoomDelta()  * _speed;
        }
    }
}
