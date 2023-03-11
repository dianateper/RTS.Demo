using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Network
{
   public class MatchmakerView : MonoBehaviour
   {
      [SerializeField] private TMP_InputField _roomNameInputField;
      [SerializeField] private Button _joinRoomButton;
      [SerializeField] private Button _createRoomButton;

      public event Action<string> OnCreateRoom;
      public event Action<string> OnJoinRoom;
   
      private void Awake()
      {
         _joinRoomButton.onClick.AddListener(JoinRoom);
         _createRoomButton.onClick.AddListener(CreateRoom);
      }

      private void CreateRoom()
      {
         if (RoomNameIsValid) OnCreateRoom?.Invoke(_roomNameInputField.text);
      }

      private void JoinRoom()
      {
         if (RoomNameIsValid) OnJoinRoom?.Invoke(_roomNameInputField.text);
      }

      private bool RoomNameIsValid => string.IsNullOrEmpty(_roomNameInputField.text) == false;
   }
}
