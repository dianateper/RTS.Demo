using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public class ProgressRenderer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void AnimateProgress(int startValue, float duration)
        {
           Utils.AnimateImageFill(_image, startValue, 1, duration);
        }
    }
}