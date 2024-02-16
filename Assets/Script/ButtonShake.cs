using System;
using System.Collections;
using System.Collections.Generic;
using Febucci.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonShake : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Button Button;
    [SerializeField] private TextMeshProUGUI TextPlay1, TextPlay2;
    
    public void OnPointerClick(PointerEventData eventData)
    {}

    public void OnPointerEnter(PointerEventData eventData)
    {
        TextPlay1.GetComponent<TextAnimator>().effectIntensityMultiplier = 10;
        TextPlay2.GetComponent<TextAnimator>().effectIntensityMultiplier = 10;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TextPlay1.GetComponent<TextAnimator>().effectIntensityMultiplier = 0;
        TextPlay2.GetComponent<TextAnimator>().effectIntensityMultiplier = 0;
    }
}

