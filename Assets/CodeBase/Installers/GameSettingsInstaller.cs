using CodeBase.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    [CreateAssetMenu(menuName = "RTS/Game Settings", fileName = "Game Settings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
    {
        [SerializeField] private PlayerSettings playerSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(playerSettings).IfNotBound();
        }
    }
}