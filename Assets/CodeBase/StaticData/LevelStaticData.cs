using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(menuName = "RTS/Create Level Static Data", fileName = "LevelStaticData")]
    public class LevelStaticData : ScriptableObject
    {
        [SerializeField] private Vector3[] _spawnPoints;
        public Vector3[] SpawnPoints => _spawnPoints;
        
        public void SetSpawnPoints(Vector3[] points)
        {
            _spawnPoints = points;
        }
    }
}