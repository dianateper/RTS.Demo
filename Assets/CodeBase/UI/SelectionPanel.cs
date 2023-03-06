using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.UI
{
    public class SelectionPanel : MonoBehaviour
    {
        [SerializeField] public List<Canvas> _panels;

        public void SelectPanel(Canvas selected)
        {
            if (selected.enabled)
            {
                selected.enabled = false;
                return;
            }
            
            foreach (var panel in _panels)
                panel.enabled = false;

            selected.enabled = true;
        }
    }
}
