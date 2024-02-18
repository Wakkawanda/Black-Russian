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
        [SerializeField] private SpawnPoint[] spawnPoints;
        [SerializeField] private TargetSpawnPoint[] targetSpawnPoints1;
        [SerializeField] private TargetSpawnPoint[] targetSpawnPoints2;
        [SerializeField] private TargetSpawnPoint[] targetSpawnPoints3;
            
        private List<BottleMove> bottles = new List<BottleMove>();

        private Coroutine coroutine;
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

            currentStepSpeedUp = stepSpeedUp;
        }

        private void Start()
        {
            coroutine = StartCoroutine(StartSpawn());
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

        public void StopSpawn()
        {
            StopCoroutine(coroutine);
            handMove.GameOverScore.text = $"{handMove.Score.text}";
            int.TryParse(PlayerPrefs.GetString("keyScore"), out int value);
            if (value < int.Parse(handMove.Score.text))
            {
                PlayerPrefs.SetString("keyScore", $"{handMove.Score}");
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
                    bottleMove.playtime = -3f;
                    bottleMove.transform.position = spawnPoints[2].transform.position;
                    bottleMove.gameObject.SetActive(true);
                    bottles[0].OnMove(handMove.BottlePosition);
                    firstBottle = bottleMove;
                    isFirst = true;
                }

                yield return null;
            }

            while (mainSound.volume > 0.3f)
            {
                mainSound.volume -= 0.06f;
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitUntil(() => handMove.isIFirstDrink);
            
            firstBottle.playtime = 2;

            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.01f;
                yield return null;
            }
            
            while (true)
            {
                int indexBottle = Random.Range(0, bottles.Count);
                int indexSpawnPoint = Random.Range(0, spawnPoints.Length);
                int indexTargetSpawnPoint1 = Random.Range(0, targetSpawnPoints1.Length);
                int indexTargetSpawnPoint2 = Random.Range(0, targetSpawnPoints2.Length);
                int indexTargetSpawnPoint3 = Random.Range(0, targetSpawnPoints3.Length);

                if (TryGetBottle(out BottleMove bottleMove, indexBottle))
                {
                    bottleMove.transform.position = spawnPoints[indexSpawnPoint].transform.position;
                    bottleMove.gameObject.SetActive(true);

                    int targetPoint1 = barUI.Slider.fillAmount >= 0.75f ? indexTargetSpawnPoint1 : indexSpawnPoint;
                    int targetPoint2 = barUI.Slider.fillAmount >= 0.75f ? indexTargetSpawnPoint2 : indexSpawnPoint;
                    int targetPoint3 = barUI.Slider.fillAmount >= 0.75f ? indexTargetSpawnPoint3 : indexSpawnPoint;
                    
                    bottles[indexBottle].OnMove(targetSpawnPoints1[targetPoint1].transform.position, targetSpawnPoints2[targetPoint2].transform.position,
                        targetSpawnPoints3[targetPoint3].transform.position);
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