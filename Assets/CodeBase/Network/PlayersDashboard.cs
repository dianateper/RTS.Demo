using System.Collections.Generic;
using CodeBase.PlayerLogic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace CodeBase.Network
{
    public class PlayersDashboard : MonoBehaviourPunCallbacks
    {
        private readonly Dictionary<Player, PlayerNetwork> _playerDataDict = new();
        private PlayersDashboardView _playersDashboardView;
      
        [Inject]
        public void Construct(PlayersDashboardView playersDashboardView)
        {
            _playersDashboardView = playersDashboardView;
        }

        private void Start()
        {
            UpdateDashboard();
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            if (changedProps.ContainsKey(Constants.AttackKey) || changedProps.ContainsKey(Constants.DefenseKey)) 
                UpdateDashboard();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            var player = newPlayer.TagObject as PlayerNetwork;
            if (player != null)
            {
                _playerDataDict[newPlayer] = newPlayer.TagObject as PlayerNetwork;
                _playersDashboardView.AddPlayer(newPlayer);
            }

            UpdateDashboard();
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