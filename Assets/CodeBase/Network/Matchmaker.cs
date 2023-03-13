using CodeBase.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace CodeBase.Network
{
    public class Matchmaker : MonoBehaviourPunCallbacks
    {
        [SerializeField] private Curtain _curtainPrefab;
     
        [SerializeField] private MatchmakerView _matchmakerView;
        [SerializeField] private LoginView _loginView;
     
        private const string GameVersion = "1";
        private bool _isConnecting;
        private Curtain _curtain;

        private void Awake()
        {
            _curtain = Instantiate(_curtainPrefab);
            PhotonNetwork.AutomaticallySyncScene = true;
            _matchmakerView.OnCreateRoom += CreateRoom;
            _matchmakerView.OnJoinRoom += JoinRoom;
            _loginView.OnLogin += Login;
        }

        private void OnDestroy()
        {
            _loginView.OnLogin -= Login;
            _matchmakerView.OnCreateRoom -= CreateRoom;
            _matchmakerView.OnJoinRoom -= JoinRoom;
        }
       
        public override void OnConnectedToMaster()
        {
            _loginView.gameObject.SetActive(false);
            _matchmakerView.gameObject.SetActive(true);
            _curtain.Hide();
        }

        public override void OnJoinedRoom()
        {
            _curtain.Hide();
            PhotonNetwork.LoadLevel(Constants.MainScene);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            _curtain.Hide();
            Debug.LogError($"OnCreateRoomFailed {message}");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            _curtain.Hide();
            Debug.LogError($"OnJoinRoomFailed {message}");
        }

        private void JoinRoom(string roomName)
        {
            _curtain.Show();
            PhotonNetwork.JoinRoom(roomName);
        }

        private void CreateRoom(string roomName)
        {
            _curtain.Show();
            PhotonNetwork.CreateRoom(roomName, new RoomOptions
            {
                MaxPlayers = 4
            });
        }

        private void Login(string playerName)
        {
            _curtain.Show();
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
}