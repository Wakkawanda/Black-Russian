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

        public void OnMove(Vector3 targetPosition1, Vector3 targetPosition2, Vector3 targetPosition3)
        {
            coroutine = StartCoroutine(Move(targetPosition1, targetPosition2, targetPosition3));
        }

        private IEnumerator Move(Vector3 targetPosition1, Vector3 targetPosition2, Vector3 targetPosition3)
        {
            playtime += 2f;

            bool isStage1 = false;
            bool isStage2 = true;
            bool isStage3 = true;

            while (Vector3.Distance(transform.position, targetPosition3) > 0.1f)
            {
                if (Vector3.Distance(transform.position, targetPosition1) < 2f)
                {
                    isStage1 = true;
                    isStage2 = false;
                    isStage3 = true;
                }
                if (Vector3.Distance(transform.position, targetPosition2) < 2f)
                {
                    isStage1 = true;
                    isStage2 = true;
                    isStage3 = false;
                }
                if (Vector3.Distance(transform.position, targetPosition3) < 0.1f)
                {
                    isStage1 = true;
                    isStage2 = true;
                    isStage3 = true;
                }
                
                if (Vector3.Distance(transform.position, targetPosition1) > 2f
                    && !isStage1)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(targetPosition1.x, transform.position.y, targetPosition1.z),
                        (bottleConfig.Speed + playtime) * Time.deltaTime);
                }
      
                if (Vector3.Distance(transform.position, targetPosition2) > 2f
                    && !isStage2)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(targetPosition2.x, transform.position.y, targetPosition2.z),
                        (bottleConfig.Speed + playtime) * Time.deltaTime);
                }
                
                if (Vector3.Distance(transform.position, targetPosition3) > 0.1f
                    && !isStage3)
                {
                    transform.position = Vector3.MoveTowards(transform.position,
                        new Vector3(targetPosition3.x, transform.position.y, targetPosition3.z),
                        (bottleConfig.Speed + playtime) * Time.deltaTime);
                }
                
                yield return null;
            }

            rigidbody.velocity = Vector3.zero;
            StopCoroutine(coroutine);
        }

        public void Stop()
        {
            StopCoroutine(coroutine);
        }

        public void OnMove(Transform bottlePositionPosition)
        {
            coroutine = StartCoroutine(StartMove(bottlePositionPosition));
        }

        private IEnumerator StartMove(Transform bottlePositionPosition)
        {
            while (Vector3.Distance(transform.position, bottlePositionPosition.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    new Vector3(bottlePositionPosition.position.x, transform.position.y, bottlePositionPosition.position.z),
                    (bottleConfig.Speed + playtime) * Time.deltaTime);

                yield return null;
            }
        }
    }
}