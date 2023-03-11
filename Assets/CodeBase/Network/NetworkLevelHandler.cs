using Photon.Pun;
using UnityEngine.SceneManagement;

namespace CodeBase.Network
{
    public class NetworkLevelHandler : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            if (PhotonNetwork.IsConnected == false)
            {
                SceneManager.LoadScene(Constants.LobbyScene);
            }
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
