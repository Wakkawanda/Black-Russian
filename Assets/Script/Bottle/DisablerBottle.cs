using UnityEngine;

namespace Script.Bottle
{
    public class DisablerBottle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<BottleMove>())
                other.gameObject.SetActive(false);
        }
    }
}