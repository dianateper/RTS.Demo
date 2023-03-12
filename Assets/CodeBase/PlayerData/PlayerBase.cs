using System;
using System.Collections.Generic;
using CodeBase.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Zenject;

namespace CodeBase.PlayerData
{
    public class PlayerBase : IPlayerBase, IOnEventCallback, IDisposable
    {
        private const byte MakeAllianceEventCode = 1;
        private const byte AttackEventCode = 2;

        private readonly List<Player> _alliance = new();
        private readonly List<Player> _enemies = new();

        private IPlayerStats _playerStats;
        private readonly PopupSystem _popupSystem;

        public List<Player> Alliance => _alliance;

        public List<Player> Enemies => _enemies;
        
        [Inject]
        public PlayerBase(IPlayerStats playerStats, PopupSystem popupSystem)
        {
            _playerStats = playerStats;
            _popupSystem = popupSystem;
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void MakeAlliance(Player player)
        {
            _alliance.Add(player);
            RaiseEventOptions options = new RaiseEventOptions { TargetActors = new[] { player.ActorNumber } };
            PhotonNetwork.RaiseEvent(MakeAllianceEventCode, PhotonNetwork.LocalPlayer.UserId, options,
                SendOptions.SendReliable);
        }

        public void Attack(Player player)
        {
            _enemies.Add(player);
            RaiseEventOptions options = new RaiseEventOptions { TargetActors = new[] { player.ActorNumber } };
            PhotonNetwork.RaiseEvent(AttackEventCode, PhotonNetwork.LocalPlayer.UserId, options,
                SendOptions.SendReliable);
        }

        public void OnEvent(EventData photonEvent)
        {
            byte eventCode = photonEvent.Code;
            if (eventCode == MakeAllianceEventCode)
            {
                var sender = PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.Sender);
                _popupSystem.ShowPopup("Alliance event", $"{sender.NickName} make an alliance");
            }
            else if (eventCode == AttackEventCode)
            {
                var sender = PhotonNetwork.CurrentRoom.GetPlayer(photonEvent.Sender);
                _popupSystem.ShowPopup("Attack event", $"{sender.NickName} attack you");
            }
        }

        public void Dispose()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }
    }
}