using System.Collections;
using UnityEngine;

namespace Script.Bottle
{
    public class BottleMove : MonoBehaviour
    {
        [SerializeField] private Spawn.Bottle bottleConfig;
        
        private Rigidbody rigidbody;
        private Coroutine coroutine;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        public void OnMove(Vector3 targetPosition)
        {
            coroutine = StartCoroutine(Move(targetPosition));
        }

        private IEnumerator Move(Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(targetPosition.x, transform.position.y, targetPosition.z),
                    bottleConfig.Speed * Time.deltaTime);
                yield return null;
            }

            rigidbody.velocity = Vector3.zero;
            StopCoroutine(coroutine);
        }

        public void Stop()
        {
            StopCoroutine(coroutine);
        }
    }
}