using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI
{
    public class PopupSystem : MonoBehaviour
    {
        [SerializeField] private Popup _popup;

        private Queue<PopupData> _popupQueue = new();

        private void OnEnable()
        {
            _popup.OnHide += HidePopup;
        }

        private void OnDisable()
        {
            _popup.OnHide -= HidePopup;
        }

        public void ShowPopup(string title, string message)
        {
            _popupQueue.Enqueue(new PopupData(title, message));
            ShowNextPopup();
        }

        private void ShowNextPopup()
        {
            if (_popupQueue.Count > 0)
            {
                var data = _popupQueue.Peek();
                _popup.Construct(data);
                _popup.SetCanvasAlpha(1);
            }
        }

        private void HidePopup()
        {
            _popupQueue.Dequeue();
            _popup.SetCanvasAlpha(0);
            if (_popupQueue.Count > 0) 
                ShowNextPopup();
        }
    }
}