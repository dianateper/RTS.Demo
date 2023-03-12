using System.Collections.Generic;
using CodeBase.StaticData;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace CodeBase.Network
{
    public class PlayersDashboard : MonoBehaviourPunCallbacks
    {
        private readonly Dictionary<Player, IPlayerStats> _playerDataDict = new();
        private PlayersDashboardView _playersDashboardView;
        private PlayerSettings _playerSettings;

        [Inject]
        public void Construct(PlayerSettings playerSettings, PlayersDashboardView playersDashboardView)
        {
            _playerSettings = playerSettings;
            _playersDashboardView = playersDashboardView;
            playersDashboardView.Initialize();
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.ContainsKey(Constants.AttackKey) || changedProps.ContainsKey(Constants.DefenseKey)) 
                UpdateDashboard();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            IPlayerStats playerData = new PlayerStats(_playerSettings);
            _playerDataDict[newPlayer] = playerData;
            _playersDashboardView.AddPlayer(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            _playerDataDict.Remove(otherPlayer);
            _playersDashboardView.RemovePLayer(otherPlayer);
        }
        
        private void UpdateDashboard()
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                int attack = player.CustomProperties.ContainsKey(Constants.AttackKey)
                    ? (int)player.CustomProperties[Constants.AttackKey]
                    : 0;
                int defense = player.CustomProperties.ContainsKey(Constants.DefenseKey)
                    ? (int)player.CustomProperties[Constants.DefenseKey]
                    : 0;
                _playersDashboardView.UpdatePlayerStats(player, attack, defense);   
            }
        }
    }
}