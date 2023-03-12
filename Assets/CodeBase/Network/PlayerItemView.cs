using TMPro;
using UnityEngine;

namespace CodeBase.Network
{
    internal class PlayerItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private TMP_Text _attack;
        [SerializeField] private TMP_Text _defence;
        
        public void SetNickName(string text)
        {
            _nickName.text = text;
        }

        public void SetAttack(int attack)
        {
            _attack.text = attack.ToString();
        }

        public void SetDefense(int defense)
        {
            _defence.text = defense.ToString();
        }
    }
}