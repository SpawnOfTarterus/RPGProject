using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistantObjectsSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistantObjectPrefab = null;

        static bool hasSpawned = false;

        private void Awake()
        {
            if (hasSpawned) { return; }
            SpawnObjects();
            hasSpawned = true;
        }

        private void SpawnObjects()
        {
            GameObject spawnedObjects = Instantiate(persistantObjectPrefab);
            DontDestroyOnLoad(spawnedObjects);
        }
    }
}
