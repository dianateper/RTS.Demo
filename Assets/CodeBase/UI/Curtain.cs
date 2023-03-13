using DG.Tweening;
using UnityEngine;

namespace CodeBase.UI
{
    public class Curtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private const float Duration = 1f;

        public void Show()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, Duration);
        }

        public void Hide()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.DOFade(0, Duration);
        }
    }
}
