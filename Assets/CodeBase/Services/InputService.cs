using System;
using UnityEngine;

namespace CodeBase.Services
{
    public class InputService : MonoBehaviour, IInputService
    {
        public event Action OnUnitPlace;
        public event Action OnUnitSelect;
        public event Action OnCancel;
        public event Action OnToggleMenu;

        public float GetHorizontalMovement() => Input.GetAxisRaw("Horizontal");

        public float GetVerticalMovement() => Input.GetAxisRaw("Vertical");
        
        public Vector3 PointerPosition() => Input.mousePosition;

        public float GetZoomDelta() => Input.GetAxis("Mouse ScrollWheel");

        public bool UnitRotate(out float delta)
        {
            delta = Input.GetAxis("Mouse X");
            return Input.GetKey(KeyCode.LeftShift);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) 
                OnUnitPlace?.Invoke();
            
            if (Input.GetMouseButtonDown(1)) 
                OnUnitSelect?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.Escape)) 
                OnCancel?.Invoke();
            
            if(Input.GetKeyDown(KeyCode.Tab))
                OnToggleMenu?.Invoke();
        }
    }
}