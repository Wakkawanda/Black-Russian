using System;
using UnityEngine;

namespace Script.Spawn
{
    public class Spawner : MonoBehaviour
    {
        private SpawnPoint[] spawnPoints;

        private void Awake()
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>();
        }
    }
}