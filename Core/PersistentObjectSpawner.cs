using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        
        [SerializeField] GameObject persistentObjectPrefab;

        static bool hasSpawn = false;
        
        private void Awake()
        {
            if(hasSpawn) return;
            SpawPersistentObjects();
            hasSpawn = true;   
        }

        private void SpawPersistentObjects()
        {
            GameObject persistentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }

    }

}
