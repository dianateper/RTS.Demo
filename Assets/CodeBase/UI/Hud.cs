using CodeBase.StaticData;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private TMP_Text gold;
        [SerializeField] private TMP_Text unitsStats;

        private IPlayerStats _playerStats;

        [Inject]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
            _playerStats.OnUnitStatsChanged += UpdateStats;
        }

        private void OnDestroy()
        {
            _playerStats.OnUnitStatsChanged -= UpdateStats;
        }

        private void UpdateStats()
        {
            gold.text = $"Gold: {_playerStats.Gold}";
            unitsStats.text = string.Empty;
            foreach (var stats in _playerStats.UnitsCount)
            {
                unitsStats.text += $"{stats.Key}: {stats.Value}\n";
            }
        }
    }
}
