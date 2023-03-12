using CodeBase.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        { 
            _inputService = inputService;
        }
      
        private void OnEnable()
        {
            _inputService.OnToggleMenu += Toggle;
        }

        private void OnDisable()
        {
            _inputService.OnToggleMenu -= Toggle;
        }

        private void Toggle()
        {
            if (_container.activeSelf == false)
                Show();
            else 
                Hide();
        }

        private void Show() => _container.SetActive(true);

        private void Hide() => _container.SetActive(false);
    }
}