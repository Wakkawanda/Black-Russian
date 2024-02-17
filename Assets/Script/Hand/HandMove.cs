using System;
using System.Collections;
using Script.Bottle;
using Script.Spawn;
using TMPro;
using UnityEngine;

namespace Script.Hand
{
    public class HandMove : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float drinkSpeed;
        [SerializeField] private float speedRotate;
        [SerializeField] private Transform targetPositionLeft;
        [SerializeField] private Transform targetPositionRight;
        [SerializeField] private Transform drinkPosition;
        [SerializeField] private Transform bottlePosition;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private BarUI.BarUI barUI;
        [SerializeField] private AudioSource audioSource;

        private bool isIDrink;
        public bool isIFirstDrink { get; private set; }

        public event Action GameOver;

        private void Start()
        {
            score.text = "0";
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A) && !isIDrink)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPositionLeft.position, speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) && !isIDrink)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPositionRight.position, speed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BottleMove bottleMove) && !isIDrink)
            {
                isIDrink = true;
                bottleMove.Stop();
                StartCoroutine(Drink(bottleMove));
            }
        }

        private IEnumerator Drink(BottleMove bottle)
        {
            Vector3 startPosition = transform.position;
            Vector3 startRotation = bottle.transform.rotation.eulerAngles;
            Vector3 targetRotateBottle = new Vector3(-bottle.transform.eulerAngles.x, bottle.transform.eulerAngles.y, bottle.transform.eulerAngles.z);

            while (Vector3.Distance(transform.position, drinkPosition.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, drinkPosition.position, drinkSpeed * Time.deltaTime);
                bottle.transform.position = Vector3.MoveTowards(bottle.transform.position, bottlePosition.position, drinkSpeed * Time.deltaTime);
                bottle.transform.Rotate(targetRotateBottle * speedRotate * Time.deltaTime);
                yield return null;
            }
            
            Debug.Log("Выпил");
            score.text = $"{int.Parse(score.text) + bottle.BottleConfig.Score}";
            audioSource.PlayOneShot(bottle.BottleConfig.AudioClip);
            isIFirstDrink = true;

            if (bottle.BottleConfig.TypeDrink == TypeDrink.Negative)
            {
                barUI.ChangeSlider(-bottle.BottleConfig.Power);
            }
            else if (bottle.BottleConfig.TypeDrink == TypeDrink.Positive)
            {
                barUI.ChangeSlider(bottle.BottleConfig.Power);
            }
            else if (bottle.BottleConfig.TypeDrink == TypeDrink.DeadDrink)
            {
                GameOver?.Invoke();
                Debug.Log("Смерть");
            }

            bottle.gameObject.SetActive(false);
            bottle.transform.eulerAngles = startRotation;

            while (Math.Abs(transform.position.z - startPosition.z) > 0.1f)
            {
                Vector3 nextTransformPosition = Vector3.MoveTowards(transform.position, startPosition, drinkSpeed * Time.deltaTime);
                transform.position =
                    new Vector3(transform.position.x, nextTransformPosition.y, nextTransformPosition.z);
                yield return null;
            }
            
            Debug.Log("Вернулся");
            isIDrink = false;
        }

        public void ChangeSpeed(float targetSpeed)
        {
            speed = targetSpeed;
        }
    }
}