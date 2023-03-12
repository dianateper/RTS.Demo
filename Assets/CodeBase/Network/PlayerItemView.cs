using CodeBase.PlayerData;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Network
{
    internal class PlayerItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nickName;
        [SerializeField] private TMP_Text _attack;
        [SerializeField] private TMP_Text _defence;
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _allianceButton;

        private Player _player;
        private IPlayerBase _playerBase;
     
        private void Start()
        {
            _attackButton.onClick.AddListener(AttackPlayer);
            _allianceButton.onClick.AddListener(MakeAlliance);
        }

        public void Construct(Player player, IPlayerBase playerBase)
        {
            _player = player;
            _playerBase = playerBase;
            _nickName.text = _player.NickName;
        }

        public void SetAttack(int attack)
        {
            _attack.text = attack.ToString();
        }

        public void SetDefense(int defense)
        {
            _defence.text = defense.ToString();
        }

        private void MakeAlliance()
        { 
            _playerBase.MakeAlliance(_player);
        }

        private void AttackPlayer()
        {
            _playerBase.Attack(_player);
        }
    }
}