using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button play, autors, exit;
    
    public void OnEnable()
    {   
        play.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        exit.onClick.AddListener(Application.Quit);
        autors.onClick.AddListener(AutorsClick);
    }

    private void AutorsClick()
    {
        
    }


    public void OnDisable()
    {
        
    }
}
