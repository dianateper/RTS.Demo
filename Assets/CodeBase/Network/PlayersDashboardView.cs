using System.Collections.Generic;
using CodeBase.PlayerLogic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Zenject;

namespace CodeBase.Network
{
    public class PlayersDashboardView : MonoBehaviour
    {
        [SerializeField] private PlayerItemView _playerViewTemplate;
        [SerializeField] private RectTransform _parent;

        private readonly Dictionary<Player, PlayerItemView> _players = new();
        private IPlayerBase _playerBase;

        [Inject]
        public void Construct(IPlayerBase playerBase)
        {
            _playerBase = playerBase;
        }

        private void Start()
        {
            LoadPlayers();
        }

        private void LoadPlayers()
        {
            var players = PhotonNetwork.PlayerList;

            foreach (var player in players)
            {
                if (player.Equals(PhotonNetwork.LocalPlayer) == false && _players.ContainsKey(player) == false)
                {
                    var playerUI = Instantiate(_playerViewTemplate, _parent);
                    playerUI.gameObject.SetActive(true);
                    playerUI.Construct(player, _playerBase);
                    _players.Add(player, playerUI);
                }
            }
        }

        public void RemovePLayer(Player otherPlayer)
        {
            if (_players.ContainsKey(otherPlayer))
            {
                var playerObject = _players[otherPlayer];
                _players.Remove(otherPlayer);
                Destroy(playerObject);
            }
        }

        public void AddPlayer(Player newPlayer)
        {
            if (newPlayer.Equals(PhotonNetwork.LocalPlayer) == false && _players.ContainsKey(newPlayer) == false)
            {
                var playerUI = Instantiate(_playerViewTemplate, _parent);
                playerUI.Construct(newPlayer, _playerBase);
                playerUI.gameObject.SetActive(true);
                _players.Add(newPlayer, playerUI);
            }
        }

        public void UpdatePlayerStats(Player player, int attack, int defense)
        {
            if (_players.ContainsKey(player))
            {
                _players[player].SetAttack(attack);
                _players[player].SetDefense(defense);   
            }
        }
    }
}