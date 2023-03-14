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
        private PlayerNetwork _playerNetwork;
        
        public List<Player> Alliance => _alliance;
        public List<Player> Enemies => _enemies;
        public IPlayerStats PlayerStats => _playerNetwork.PlayerStats;

        [Inject]
        public PlayerBase(PopupSystem popupSystem)
        {
            _popupSystem = popupSystem;
        }

        public void SetPlayerNetwork(PlayerNetwork playerNetwork)
        {
            _playerNetwork = playerNetwork; 
            _playerNetwork.Construct(_popupSystem);
        }
        
        public void MakeAlliance(Player player)
        {
            _alliance.Add(player);
            _playerNetwork.MakeAlliance(player);
        }

        public void Attack(Player player)
        {
            _enemies.Add(player);
            _playerNetwork.Attack(player);
        }
    }
}