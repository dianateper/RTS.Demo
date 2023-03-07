using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gold;
        [SerializeField] private TMP_Text _unitsStats;
        [SerializeField] private Image _defense;
        [SerializeField] private Image _attack;

        private IPlayerStats _playerStats;

        [Inject]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
            _playerStats.OnUnitStatsChanged += UpdateStats;
            _playerStats.OnResourceChanged += UpdateResource;
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
            _defense.fillAmount = _playerStats.Defense;
            _attack.fillAmount = _playerStats.Attack;
            _gold.text = $"Gold: {_playerStats.Gold}";
        }

        private void UpdateStats()
        {
            _gold.text = $"Gold: {_playerStats.Gold}";
            _unitsStats.text = string.Empty;
            foreach (var stats in _playerStats.UnitsCount)
            {
                _unitsStats.text += $"{stats.Key}: {stats.Value}\n";
            }
        }
    }
}
