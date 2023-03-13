using Photon.Pun;
using UnityEngine;
using Zenject;

namespace CodeBase.PlayerLogic
{
    [CreateAssetMenu(menuName = "RTS/Create Player Factory", fileName = "PlayerFactory")]
    public class PlayerFactory : ScriptableObject
    {
        [SerializeField] private PlayerMono _playerBasePrefab;
        private IPlayerBase _playerBase;
        
        [Inject]
        public void Construct(IPlayerBase playerBase)
        {
            _playerBase = playerBase;
        }

        public PlayerMono CreatePlayer()
        {
            var player =  PhotonNetwork
                .Instantiate(_playerBasePrefab.name, Vector3.zero, Quaternion.identity, 0).GetComponent<PlayerMono>();
            _playerBase.SetPlayerMono(player);
            return player;
        }
    }
}