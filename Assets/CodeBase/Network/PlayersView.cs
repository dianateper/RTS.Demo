using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.Network
{
    public class PlayersView : MonoBehaviourPunCallbacks
    {
        [SerializeField] private PlayerItemView _playerNickTemplate;
        [SerializeField] private RectTransform _parent;

        private Dictionary<Player, GameObject> _players;

        private void Start()
        {
            _players = new Dictionary<Player, GameObject>();
            LoadPlayers();
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
                    _players.Add(player, playerUI.gameObject);
                }
            }
        }
        
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.LogFormat("OnPlayerEnteredRoom() {0}", newPlayer.NickName);
            AddPlayer(newPlayer);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            Debug.LogFormat("OnPlayerLeftRoom() {0}", otherPlayer.NickName);
            RemovePLayer(otherPlayer);
        }

        private void RemovePLayer(Player otherPlayer)
        {
            var playerObject = _players[otherPlayer];
            _players.Remove(otherPlayer);
            Destroy(playerObject);
        }

        private void AddPlayer(Player newPlayer)
        {
            if (_players.ContainsKey(newPlayer) == false)
            {
                var playerUI = Instantiate(_playerNickTemplate, _parent);
                playerUI.SetNickName(newPlayer.NickName);
                playerUI.gameObject.SetActive(true);
                _players.Add(newPlayer, playerUI.gameObject);
            }
        }
    }
}