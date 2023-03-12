using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    [RequireComponent(typeof(TMP_InputField))]
    [RequireComponent(typeof(Image))]
    public class InputFieldValidationRenderer : MonoBehaviour
    {
        [SerializeField] private Button _submitButton;
        private TMP_InputField _inputField;
        private Image _inputFieldImage;

        private readonly Color _invalidColor = Color.red;
        private Color _originColor;
        
        private void Awake()
        {
            _submitButton.onClick.AddListener(Validate);
            _inputField = GetComponent<TMP_InputField>();
            _inputFieldImage = GetComponent<Image>();
            _originColor = _inputFieldImage.color;
        }

        private void Validate()
        {
            _inputFieldImage.color = string.IsNullOrEmpty(_inputField.text) ? _invalidColor : _originColor;
        }
    }
}