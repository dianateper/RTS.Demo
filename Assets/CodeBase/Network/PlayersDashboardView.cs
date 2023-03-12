using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.Network
{
    public class PlayersDashboardView : MonoBehaviour
    {
        [SerializeField] private PlayerItemView _playerNickTemplate;
        [SerializeField] private RectTransform _parent;

        private Dictionary<Player, PlayerItemView> _players;

        public void Initialize()
        {
            _players = new Dictionary<Player, PlayerItemView>();
            LoadPlayers();
            gameObject.SetActive(false);
        }

        private void LoadPlayers()
        {
            var players = PhotonNetwork.PlayerList;

            foreach (var player in players)
            {
                if (_players.ContainsKey(player) == false)
                {
                    var playerUI = Instantiate(_playerNickTemplate, _parent);
                    playerUI.gameObject.SetActive(true);
                    playerUI.SetNickName(player.NickName);
                    _players.Add(player, playerUI);
                }
            }
        }

        public void RemovePLayer(Player otherPlayer)
        {
            var playerObject = _players[otherPlayer];
            _players.Remove(otherPlayer);
            Destroy(playerObject);
        }

        public void AddPlayer(Player newPlayer)
        {
            if (_players.ContainsKey(newPlayer) == false)
            {
                var playerUI = Instantiate(_playerNickTemplate, _parent);
                playerUI.SetNickName(newPlayer.NickName);
                playerUI.gameObject.SetActive(true);
                _players.Add(newPlayer, playerUI);
            }
        }

        public void UpdatePlayerStats(Player player, int attack, int defense)
        {
            _players[player].SetAttack(attack);
            _players[player].SetDefense(defense);
        }
    }
}