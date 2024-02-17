using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script.BarUI
{
    public class BarUI : MonoBehaviour
    {
        [SerializeField] private Image slider;

        private void Start()
        {
            StartCoroutine(RunningSobriety());
        }

        private IEnumerator RunningSobriety()
        {
            while (true)
            {
                slider.fillAmount -= 0.01f;
                yield return new WaitForSeconds(2);
            }
        }
    }
}