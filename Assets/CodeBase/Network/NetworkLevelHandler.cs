using CodeBase.PlayerLogic;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Network
{
    public class NetworkLevelHandler : MonoBehaviourPunCallbacks
    {
        private PlayerFactory _playerFactory;

        [Inject]
        public void Construct(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }
        
        private void Start()
        {
            if (PhotonNetwork.IsConnected == false)
            {
                SceneManager.LoadScene(Constants.LobbyScene);
            }

            _playerFactory.CreatePlayer();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadScene(Constants.LobbyScene);
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }
    }
}
