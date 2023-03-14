using System.Linq;
using CodeBase.StaticData;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(LevelStaticData))]
    public class LevelStaticDataEditor : UnityEditor.Editor
    {
        private LevelStaticData _levelStaticData;
        public override void OnInspectorGUI()
        {
            _levelStaticData = target as LevelStaticData;
            DrawDefaultInspector();
            
            if (GUILayout.Button("Collect spawner points"))
            {
                CollectSpawnerPoints();
            }
        }

        private void CollectSpawnerPoints()
        {
            _levelStaticData.SetSpawnPoints(GameObject.FindGameObjectsWithTag("SpawnPoint")
                .Select(p => p.transform.position).ToArray());
        }
    }
}