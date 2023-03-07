using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UnitsSystem.UnitLogic
{
    public class ProgressRenderer : MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void UpdateProgress(float progress)
        {
            _image.fillAmount = progress;
        }
    }
}