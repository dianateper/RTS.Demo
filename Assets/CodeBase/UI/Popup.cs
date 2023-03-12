using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public struct PopupData
    {
        public string Title { get; }
        public string Message { get; }

        public PopupData(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
    
    public class Popup : MonoBehaviour
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private Button _closeButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        public event Action OnHide;
        
        private void Start()
        {
            _closeButton.onClick.AddListener(Close);
        }

        public void Construct(PopupData popupData)
        {
            _title.text = popupData.Title;
            _message.text = popupData.Message;
        }

        public void SetCanvasAlpha(int alpha)
        {
            _canvasGroup.alpha = alpha;
        }

        private void Close()
        {
            OnHide?.Invoke();
        }
    }
}
