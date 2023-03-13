using CodeBase.PlayerData;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gold;
        [SerializeField] private TMP_Text _nickname;
        [SerializeField] private Image _attack;
        [SerializeField] private Image _defense;
      
        private IPlayerStats _playerStats;
       
        [Inject]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
            _playerStats.OnGoldChanged += UpdateGoldInfo;
            _playerStats.OnUnitsChanged += UpdateUnitsInfo;
            _nickname.text = PhotonNetwork.LocalPlayer.NickName;
            UpdateUnitsInfo();
            UpdateGoldInfo();
        }

        private void OnDestroy()
        {
            _playerStats.OnGoldChanged -= UpdateGoldInfo;
            _playerStats.OnUnitsChanged -= UpdateUnitsInfo;
        }

        private void UpdateUnitsInfo()
        {
            Utils.AnimateImageFill(_attack, _playerStats.AttackPercent);
            Utils.AnimateImageFill(_defense, _playerStats.DefensePercent);
        }
        
        private void UpdateGoldInfo()
        {
            _gold.text = $"{_playerStats.Gold}";
        }
    }
}
