using System;
using System.Collections;
using Script.Hand;
using Script.Spawn;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Script.BarUI
{
    public class BarUI : MonoBehaviour
    {
        [SerializeField] private Image slider;
        [SerializeField] private HandMove handMove;
        [SerializeField] private ParticleSystem particleSystemUp;
        [SerializeField] private ParticleSystem particleSystemDown;
        [SerializeField] private PostProcessVolume postProcessVolume;
        [SerializeField] private CanvasGroup mainCanvasGroup;
        [SerializeField] private CanvasGroup barCanvasGroup;
        [SerializeField] private GameObject end1;
        [SerializeField] private GameObject end2;
        [SerializeField] private GameObject end3;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioSource gameOver;
        [SerializeField] private Spawner spawner;
        
        private const string RewardKey1 = "RewardKey1";
        private const string RewardKey2 = "RewardKey2";
        private const string RewardKey4 = "RewardKey4";

        private Vignette vignette;
        private LensDistortion lensDistortion;
        private Coroutine coroutine;
        private float delaySecondsSpawn = 1.5f;
        private float currentGameTime = 0;
        private float stepSpeedUp = 25;
        private float currentStepSpeedUp;

        public Image Slider => slider;

        private void Awake()
        {
            mainCanvasGroup.alpha = 0;
            mainCanvasGroup.blocksRaycasts = false;
            mainCanvasGroup.interactable = false;
            currentGameTime = 0;
            lensDistortion = postProcessVolume.profile.GetSetting<LensDistortion>();
            vignette = postProcessVolume.profile.GetSetting<Vignette>();
        }

        private void Start()
        {
            coroutine = StartCoroutine(RunningSobriety());
        }

        private void Update()
        {
            currentGameTime += Time.deltaTime;
        }

        private IEnumerator RunningSobriety()
        {
            yield return new WaitUntil(() => handMove.isIFirstDrink);
            
            while (true)
            {
                slider.fillAmount -= 0.01f;
                
                if (slider.fillAmount > 0.75f)
                {
                    float targetValue = lensDistortion.intensity.value - 50f;
                    
                    while (Math.Abs(lensDistortion.intensity.value - targetValue) > 0.01f)
                    {
                        lensDistortion.intensity.value -= 0.1f;
                        yield return null;
                    }
                }
                
                if (slider.fillAmount < 0.75f)
                {
                    while (lensDistortion.intensity.value is < 0 and < 100)
                    {
                        lensDistortion.intensity.value += 1f;
                        yield return null;
                    }

                    if (lensDistortion.intensity.value > 0)
                    {
                        lensDistortion.intensity.value = 0;
                    }
                }

                if (slider.fillAmount < 0.25f)
                {
                    float targetValue = vignette.intensity.value + 0.05f;
                    
                    while (Math.Abs(vignette.intensity.value - targetValue) > 0.01f)
                    {
                        vignette.intensity.value += 0.01f;
                        yield return null;
                    }
                }
                
                if (slider.fillAmount > 0.25f)
                {
                    vignette.intensity.value = 0f;
                }

                if (slider.fillAmount > 0.7f)
                {
                    particleSystemUp.gameObject.SetActive(true);
                }
                else
                {
                    particleSystemUp.gameObject.SetActive(false);
                }

                if (slider.fillAmount < 0.25f)
                {
                    particleSystemDown.gameObject.SetActive(true);
                }
                else
                {
                    particleSystemDown.gameObject.SetActive(false);
                }

                if (currentGameTime > currentStepSpeedUp)
                {
                    currentStepSpeedUp += stepSpeedUp;
                    if (delaySecondsSpawn > 0.3)
                        delaySecondsSpawn -= 0.3f;
                }

                if (vignette.intensity.value >= 0.8f && int.Parse(handMove.Score.text) < 1000)
                {
                    PlayerPrefs.SetInt(RewardKey1, 1);
                    end1.gameObject.SetActive(true);
                    StartCoroutine(GameOver());
                }
                
                if (lensDistortion.intensity.value < -75f && int.Parse(handMove.Score.text) < 1000)
                {
                    StartCoroutine(StartGameOver());
                }
                
                if (vignette.intensity.value >= 0.8f && int.Parse(handMove.Score.text) >= 1000)
                {
                    PlayerPrefs.SetInt(RewardKey4, 1);
                    end3.gameObject.SetActive(true);
                    StartCoroutine(GameOver());
                }
                
                if (lensDistortion.intensity.value < -75f && int.Parse(handMove.Score.text) >= 1000)
                {
                    StartCoroutine(StartGameOver());
                }

                yield return new WaitForSeconds(delaySecondsSpawn);
            }
        }

        private IEnumerator StartGameOver()
        {
            yield return new WaitForSeconds(5);

            if (lensDistortion.intensity.value < -75f && int.Parse(handMove.Score.text) < 1000)
            {
                PlayerPrefs.SetInt(RewardKey2, 1);
                end2.gameObject.SetActive(true);
                StartCoroutine(GameOver());
            }

            if (lensDistortion.intensity.value < -75f && int.Parse(handMove.Score.text) >= 1000)
            {
                PlayerPrefs.SetInt(RewardKey4, 1);
                end3.gameObject.SetActive(true);
                StartCoroutine(GameOver());
            }
        }

        public IEnumerator GameOver()
        {
            gameOver.Play();
            barCanvasGroup.alpha = 0;
            spawner.StopSpawn();
            StopCoroutine(coroutine);
            
            while (mainCanvasGroup.alpha < 1)
            {
                audioSource.volume -= 0.01f;
                mainCanvasGroup.alpha += 0.01f;
                yield return null;
            }
            
            mainCanvasGroup.blocksRaycasts = true;
            mainCanvasGroup.interactable = true;
        }

        public void ChangeSlider(int value)
        {
            slider.fillAmount += (float)value / 100;
        }
    }
}