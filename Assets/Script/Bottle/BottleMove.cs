using System.Collections;
using UnityEngine;

namespace Script.Bottle
{
    public class BottleMove : MonoBehaviour
    {
        [SerializeField] private Spawn.Bottle bottleConfig;

        public IEnumerator Move(Vector3 targetPosition)
        {
            while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, bottleConfig.Speed * Time.deltaTime);
                yield return null;
            }
            
            gameObject.SetActive(false);
        }
    }
}