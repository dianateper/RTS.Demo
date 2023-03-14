using System;
using CodeBase.PlayerLogic;
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
      
        private IPlayerBase _playerBase;
        private IPlayerStats _playerStats;
       
        [Inject]
        public void Construct(IPlayerBase playerBase)
        {
            _playerBase = playerBase;
            _nickname.text = PhotonNetwork.LocalPlayer.NickName;
        }

        private void OnEnable()
        {
            _playerStats = _playerBase.PlayerStats;
            _playerStats.OnGoldChanged += UpdateGoldInfo;
            _playerStats.OnUnitsChanged += UpdateUnitsInfo;
            UpdateUnitsInfo();
            UpdateGoldInfo();
        }

        private void OnDisable()
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
