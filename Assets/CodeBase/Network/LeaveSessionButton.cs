using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Network
{
   public class LeaveSessionButton : MonoBehaviour
   {
      [SerializeField] private Button _leaveSession;

      private NetworkLevelHandler _networkLevelHandler;
   
      [Inject]
      public void Construct(NetworkLevelHandler networkLevelHandler)
      {
         _networkLevelHandler = networkLevelHandler;
         _leaveSession.onClick.AddListener(LeaveSession);
      }

      private void LeaveSession() => _networkLevelHandler.LeaveRoom();
   }
}
