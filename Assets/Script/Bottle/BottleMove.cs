using System.Collections;
using UnityEngine;

namespace Script.Bottle
{
    public class BottleMove : MonoBehaviour
    {
        [SerializeField] private Spawn.Bottle bottleConfig;
        [SerializeField] private Material material;

        private Rigidbody rigidbody;
        private Coroutine coroutine;
        public float playtime = 1;

        public Spawn.Bottle BottleConfig => bottleConfig;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void OnRenderImage (RenderTexture source, RenderTexture destination) 
        {
            Graphics.Blit (source, destination, material);
        }

        public void OnMove(Vector3 targetPosition)
        {
            coroutine = StartCoroutine(Move(targetPosition));
        }

        private IEnumerator Move(Vector3 targetPosition)
        {
            playtime += 2f;

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(targetPosition.x, transform.position.y, targetPosition.z),
                    (bottleConfig.Speed + playtime) * Time.deltaTime);
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