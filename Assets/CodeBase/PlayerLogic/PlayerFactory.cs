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

        public PlayerNetwork CreatePlayer()
        {
            var player = PhotonNetwork
                .Instantiate(_playerNetworkPrefab.name, Vector3.zero, Quaternion.identity, 0).GetComponent<PlayerNetwork>();
            _playerBase.SetPlayerNetwork(player);
            return player;
        }
    }
}