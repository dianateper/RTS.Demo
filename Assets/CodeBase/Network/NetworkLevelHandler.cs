using System.Linq;
using CodeBase.PlayerLogic;
using CodeBase.StaticData;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CodeBase.Network
{
    public class NetworkLevelHandler : MonoBehaviourPunCallbacks
    {
        private PlayerFactory _playerFactory;
        private LevelStaticData _levelStaticData;
        private Vector3[] _spawnPoints;

        [Inject]
        public void Construct(PlayerFactory playerFactory, LevelStaticData levelStaticData)
        {
            _playerFactory = playerFactory;
            _levelStaticData = levelStaticData;
        }

        private void Awake()
        {
            if (PhotonNetwork.IsConnected == false)
            {
                SceneManager.LoadScene(Constants.LobbyScene);
            }

            _spawnPoints = _levelStaticData.SpawnPoints;
            _playerFactory.CreatePlayer(_spawnPoints);
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
