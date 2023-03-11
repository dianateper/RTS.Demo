using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Network
{
    public class LoginView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField _playerNameInput;
        [SerializeField] private Button _loginButton;

        public event Action<string> OnLogin;
        
        private void Awake()
        {
            _loginButton.onClick.AddListener(Login);
        }

        private void Login()
        {
            if (PlayerNickNameIsValid) OnLogin?.Invoke(_playerNameInput.text);
        }

        private bool PlayerNickNameIsValid  => string.IsNullOrEmpty(_playerNameInput.text) == false;
    }
}
