using System;
using System.Collections;
using Script.Hand;
using UnityEngine;
using UnityEngine.UI;

namespace Script.BarUI
{
    public class BarUI : MonoBehaviour
    {
        [SerializeField] private Image slider;
        [SerializeField] private HandMove handMove;
        [SerializeField] private ParticleSystem particleSystemUp;
        [SerializeField] private ParticleSystem particleSystemDown;
        
        private float delaySecondsSpawn = 2;
        private float currentGameTime = 0;
        private float stepSpeedUp = 25;
        private float currentStepSpeedUp;

        public Image Slider => slider;

        private void Awake()
        {
            currentGameTime = 0;
        }

        private void Start()
        {
            StartCoroutine(RunningSobriety());
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

                if (currentGameTime > currentStepSpeedUp)
                {
                    currentStepSpeedUp += stepSpeedUp;
                    if (delaySecondsSpawn > 0.3)
                        delaySecondsSpawn -= 0.3f;
                }

                if (slider.fillAmount > 0.9f)
                {
                    particleSystemUp.gameObject.SetActive(true);
                }
                else
                {
                    particleSystemUp.gameObject.SetActive(false);
                }

                if (slider.fillAmount < 0.1f)
                {
                    particleSystemDown.gameObject.SetActive(true);
                }
                else
                {
                    particleSystemDown.gameObject.SetActive(false);
                }
                
                yield return new WaitForSeconds(delaySecondsSpawn);
            }
        }

        public void ChangeSlider(int value)
        {
            slider.fillAmount += (float)value / 100;
        }
    }
}