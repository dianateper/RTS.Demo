using TMPro;
using UnityEngine;

namespace CodeBase.Network
{
    internal class PlayerItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text nickName;
        [SerializeField] private TMP_Text attack;
        [SerializeField] private TMP_Text defence;
        
        public void SetNickName(string text)
        {
            nickName.text = text;
        }
    }
}