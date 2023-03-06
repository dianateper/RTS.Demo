using CodeBase.UnitsSystem.StaticData;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public class WorldUnit : MonoBehaviour
    {
        [SerializeField] private UnitRenderer _unitOutlieRenderer;
        private Vector3 _targetPosition;
        private UnitSettings _unitSettings;
        
        public void Construct(UnitSettings unitSettings)
        {
            _unitSettings = unitSettings;
            _unitOutlieRenderer.Construct(unitSettings);
        }
        
        private void Update()
        {
            if (_targetPosition != Vector3.zero)
                transform.position = Vector3.Lerp(transform.position, _targetPosition,
                    Time.deltaTime * _unitSettings.Speed);
        }

        public void SetValid(bool isValid)
        {
            _unitOutlieRenderer.ChangeColor(isValid);
        }
        
        public void Place(Vector3 position)
        {
            transform.position = position;
            _unitOutlieRenderer.DisableRenderer();
        }

        public void Rotate(float delta)
        {
            transform.Rotate(Vector3.up, delta * _unitSettings.RotationAngle);
        }

        public void SetPosition(Vector3 position)
        {
            _targetPosition = position;
        }

        public void Select()
        {
            _unitOutlieRenderer.EnableRenderer();
        }
    }
}
