using System;
using UnityEngine;

namespace CodeBase.Services
{
    public interface IInputService
    {
        event Action OnUnitPlace;
        event Action OnUnitSelect;
        event Action OnCancel;
        float GetHorizontalMovement();
        float GetVerticalMovement();
        Vector3 PointerPosition();
        float GetZoomDelta();
        bool UnitRotate(out float delta);
    }
}