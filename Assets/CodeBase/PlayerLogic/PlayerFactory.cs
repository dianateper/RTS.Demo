using System.Linq;
using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CodeBase.PlayerLogic
{
    [CreateAssetMenu(menuName = "RTS/Create Player Factory", fileName = "PlayerFactory")]
    public class PlayerFactory : ScriptableObject
    {
        [SerializeField] private PlayerNetwork _playerNetworkPrefab;
        private IPlayerBase _playerBase;
        
        [Inject]
        public void Construct(IPlayerBase playerBase)
        {
            _playerBase = playerBase;
        }

        public PlayerNetwork CreatePlayer(Vector3[] spawnPoints)
        {
            var player = PhotonNetwork
                .Instantiate(_playerNetworkPrefab.name, Vector3.zero, Quaternion.identity, 0).GetComponent<PlayerNetwork>();
            _playerBase.SetPlayerNetwork(player);
            player.transform.position = GetRandomPosition(spawnPoints);;
            return player;
        }

        private Vector3 GetRandomPosition(Vector3[] spawnPoints)
        {
            var indexTaken = PhotonNetwork.PlayerList.Where(p => p.IsLocal == false)
                .Select(p => (p.ActorNumber - 1) % spawnPoints.Length).ToArray();
            int index;
        
            do index = Random.Range(0, spawnPoints.Length);
            while (indexTaken.Contains(index));
         
            return spawnPoints[index];
        }
    }
}