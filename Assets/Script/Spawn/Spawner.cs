using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Bottle;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Bottle> bottleConfigs;

        private List<BottleMove> bottles = new List<BottleMove>();
        private SpawnPoint[] spawnPoints;
        private TargetSpawnPoint[] targetSpawnPoints;

        private void Awake()
        {
            foreach (Bottle bottleConfig in bottleConfigs)
            {
                BottleMove bottleMove1 = Instantiate(bottleConfig.Prefab).GetComponent<BottleMove>();
                bottleMove1.gameObject.SetActive(false);
                BottleMove bottleMove2 = Instantiate(bottleConfig.Prefab).GetComponent<BottleMove>();
                bottleMove2.gameObject.SetActive(false);
                BottleMove bottleMove3 = Instantiate(bottleConfig.Prefab).GetComponent<BottleMove>();
                bottleMove3.gameObject.SetActive(false);
                
                bottles.Add(bottleMove1);
                bottles.Add(bottleMove2);
                bottles.Add(bottleMove3);
            }

            spawnPoints = GetComponentsInChildren<SpawnPoint>();
            targetSpawnPoints = GetComponentsInChildren<TargetSpawnPoint>();
        }

        private void Start()
        {
            StartCoroutine(StartSpawn());
        }

        private IEnumerator StartSpawn()
        {
            while (true)
            {
                int indexBottle = Random.Range(0, bottles.Count);
                int indexSpawnPoint = Random.Range(0, spawnPoints.Length);
                int indexTargetSpawnPoint = Random.Range(0, targetSpawnPoints.Length);

                if (TryGetBottle(out BottleMove bottleMove, indexBottle))
                {
                    bottleMove.transform.position = spawnPoints[indexSpawnPoint].transform.position;
                    bottleMove.gameObject.SetActive(true);
                    bottles[indexBottle].OnMove(targetSpawnPoints[indexSpawnPoint].transform.position);
                }

                yield return new WaitForSeconds(2);
            }
        }

        private bool TryGetBottle(out BottleMove bottleMove, int indexElement)
        {
            bottleMove = bottles.ElementAtOrDefault(indexElement);

            if (bottleMove.gameObject.activeSelf)
                bottleMove = null;

            return bottleMove != null;
        }
    }
}