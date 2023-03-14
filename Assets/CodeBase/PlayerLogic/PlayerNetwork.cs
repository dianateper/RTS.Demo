using CodeBase.UI;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.PlayerLogic
{
    [RequireComponent(typeof(PhotonView))]
    [RequireComponent(typeof(PlayerStats))]
    public class PlayerNetwork : MonoBehaviourPunCallbacks, IOnEventCallback, IPunObservable
    {
        [SerializeField] private PhotonView _photonView;
        [SerializeField] private PlayerStats _playerStats;
        private PopupSystem _popupSystem;
      
        private const byte MakeAllianceEventCode = 1;
        private const byte AttackEventCode = 2;
        
        public PhotonView PhotonView => _photonView;
        public IPlayerStats PlayerStats => _playerStats;

        public void Construct(PopupSystem popupSystem, PlayerSettings playerSettings)
        {
            _playerStats.Construct(playerSettings);
            _popupSystem = popupSystem;
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnDestroy()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        public void MakeAlliance(Player player)
        {
            RaiseEventOptions options = new RaiseEventOptions { TargetActors = new[] { player.ActorNumber } };
            PhotonNetwork.RaiseEvent(MakeAllianceEventCode, PhotonNetwork.LocalPlayer.UserId, options,
                SendOptions.SendReliable);
        }

        public void Attack(Player player)
        {
            RaiseEventOptions options = new RaiseEventOptions { TargetActors = new[] { player.ActorNumber } };
            PhotonNetwork.RaiseEvent(AttackEventCode, PhotonNetwork.LocalPlayer.UserId, options,
                SendOptions.SendReliable);
        }

        public void OnEvent(EventData photonEvent)
        {
            if(_photonView.IsMine == false) return;
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

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }
    }
}