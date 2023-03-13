using DG.Tweening;
using UnityEngine.UI;

namespace CodeBase
{
    public static class Utils
    {
        private const float ImageFillSlideDuration = 1f;

        public static void AnimateImageFill(Image image, float endValue)
        {
            DOTween.To(() => image.fillAmount, x => image.fillAmount = x, endValue, ImageFillSlideDuration);
        }
        
        public static void AnimateImageFill(Image image, float startValue, float endValue, float duration = ImageFillSlideDuration)
        {
            DOTween.To(() => startValue, x => image.fillAmount = x, endValue, duration);
        }
    }
}