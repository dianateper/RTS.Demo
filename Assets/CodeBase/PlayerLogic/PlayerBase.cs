using System.Collections.Generic;
using CodeBase.UI;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace CodeBase.PlayerLogic
{
    public class PlayerBase : IPlayerBase
    {
        private static List<Vector3> _spawnPoints;

        private readonly List<Player> _alliance = new();
        private readonly List<Player> _enemies = new();

        private IPlayerStats _playerStats;
        private PopupSystem _popupSystem;
        private PlayerMono _playerMono;
        
        public List<Player> Alliance => _alliance;
        public List<Player> Enemies => _enemies;
        
        [Inject]
        public PlayerBase(IPlayerStats playerStats, PopupSystem popupSystem)
        {
            _playerStats = playerStats;
            _popupSystem = popupSystem;
        }

        public void SetPlayerMono(PlayerMono playerMono)
        {
            _playerMono = playerMono;
            Debug.Log(_popupSystem);
            _playerMono.Construct(_popupSystem);
        }

        public void MakeAlliance(Player player)
        {
            _alliance.Add(player);
            _playerMono.MakeAlliance(player);
        }

        public void Attack(Player player)
        {
            _enemies.Add(player);
            _playerMono.Attack(player);
        }
    }
}