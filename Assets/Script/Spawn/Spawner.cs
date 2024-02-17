using System;
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
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private BarUI.BarUI barUI;

        private List<BottleMove> bottles = new List<BottleMove>();
        private SpawnPoint[] spawnPoints;
        private TargetSpawnPoint[] targetSpawnPoints;
        private float delaySecondsSpawn = 2;
        private float currentGameTime = 0;
        private float stepSpeedUp = 25;
        private float currentStepSpeedUp;

        private void Awake()
        {
            canvasGroup.alpha = 0;
            
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
            
            currentStepSpeedUp = stepSpeedUp;
        }

        private void Start()
        {
            StartCoroutine(StartSpawn());
        }

        private void Update()
        {
            currentGameTime += Time.deltaTime;

            if (currentGameTime > currentStepSpeedUp)
            {
                currentStepSpeedUp += stepSpeedUp;
                if (delaySecondsSpawn > 0.6f)
                    delaySecondsSpawn -= 0.5f;
            }
        }

        private IEnumerator StartSpawn()
        {
            currentGameTime = 0;
            bool isFirst = false;
            BottleMove firstBottle = null;
            
            while (!isFirst)
            {
                if (TryGetBottle(out BottleMove bottleMove, 0))
                {
                    bottleMove.playtime = -7f;
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
                mainSound.volume -= 0.0211f;
                yield return new WaitForSeconds(1f);
            }

            firstBottle.playtime = 2;

            yield return new WaitUntil(() => handMove.isIFirstDrink);
            
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.01f;
                yield return null;
            }
            
            while (true)
            {
                int indexBottle = Random.Range(0, bottles.Count);
                int indexSpawnPoint = Random.Range(0, spawnPoints.Length);
                int indexTargetSpawnPoint = Random.Range(0, targetSpawnPoints.Length);

                if (TryGetBottle(out BottleMove bottleMove, indexBottle))
                {
                    bottleMove.transform.position = spawnPoints[indexSpawnPoint].transform.position;
                    bottleMove.gameObject.SetActive(true);

                    int targetPoint = barUI.Slider.fillAmount >= 0.75f ? indexTargetSpawnPoint : indexSpawnPoint;
                    bottles[indexBottle].OnMove(targetSpawnPoints[targetPoint].transform.position);
                }

                yield return new WaitForSeconds(delaySecondsSpawn);
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