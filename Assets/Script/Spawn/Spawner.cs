using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Bottle;
using Script.Hand;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Spawn
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<Bottle> bottleConfigs;
        [SerializeField] private AudioSource mainSound;
        [SerializeField] private HandMove handMove;

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
            bool isFirst = false;
            BottleMove firstBottle = null;
            
            while (!isFirst)
            {
                if (TryGetBottle(out BottleMove bottleMove, 0))
                {
                    bottleMove.BottleConfig.Speed = 1.2f;
                    bottleMove.transform.position = spawnPoints[2].transform.position;
                    bottleMove.gameObject.SetActive(true);
                    bottles[0].OnMove(targetSpawnPoints[2].transform.position);
                    firstBottle = bottleMove;
                    isFirst = true;
                }

                yield return null;
            }

            while (mainSound.volume > 0.3f)
            {
                mainSound.volume -= 0.03f;
                yield return new WaitForSeconds(0.65f);
            }

            yield return new WaitUntil(() => handMove.isIFirstDrink);
            
            firstBottle.BottleConfig.Speed = 10f;
            
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