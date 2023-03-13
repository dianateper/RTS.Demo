using CodeBase.PlayerData;
using CodeBase.StaticData;
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
            _playerStats.OnUnitStatsChanged += UpdateStats;
            _playerStats.OnResourceChanged += UpdateResource;
            _nickname.text = PhotonNetwork.LocalPlayer.NickName;
            UpdateResource();
            UpdateStats();
        }

        private void OnDestroy()
        {
            _playerStats.OnUnitStatsChanged -= UpdateStats;
            _playerStats.OnResourceChanged -= UpdateResource;
        }

        private void UpdateResource()
        {
            _gold.text = $"{_playerStats.Gold}";
            _attack.fillAmount = _playerStats.AttackPercent;
            _defense.fillAmount = _playerStats.DefensePercent;
        }

        private void UpdateStats()
        {
            _gold.text = $"{_playerStats.Gold}";
        }
    }
}
