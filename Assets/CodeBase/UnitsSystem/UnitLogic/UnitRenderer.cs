using CodeBase.UnitsSystem.StaticData;
using UnityEngine;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public class UnitRenderer : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        private UnitSettings _unitSettings;
        private MaterialPropertyBlock _propertyBlock;
        private static readonly int ColorProp = Shader.PropertyToID("_BaseColor");
        
        public void Construct(UnitSettings settings)
        {
            _unitSettings = settings;
            _propertyBlock = new MaterialPropertyBlock();
            EnableRenderer();
        }
        
        public void EnableRenderer() => _renderer.enabled = true;

        public void DisableRenderer() => _renderer.enabled = false;

        public void ChangeColor(bool isValid) => ChangeColor(isValid ? _unitSettings.ValidColor : _unitSettings.InValidColor);

        private void ChangeColor(Color color)
        {
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(ColorProp, color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}