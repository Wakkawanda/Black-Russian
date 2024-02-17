using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button play, autors, exit, settings;
    [SerializeField] private GameObject n1, n2, n3;
    
    public void OnEnable()
    {   
        play.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        exit.onClick.AddListener(Application.Quit);
        autors.onClick.AddListener(AutorsClick);
        // back.onClick.AddListener(BackClick);
        settings.onClick.AddListener(SettingsClick);
    }

    private void SettingsClick()
    {
        play.gameObject.SetActive(false);
        autors.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        n1.gameObject.SetActive(false);
        n2.gameObject.SetActive(false);
        n3.gameObject.SetActive(false);
        
        
    }

    private void BackClick()
    {
        play.gameObject.SetActive(true);
        autors.gameObject.SetActive(true);
        settings.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        n1.gameObject.SetActive(true);
        n2.gameObject.SetActive(true);
        n3.gameObject.SetActive(true);
        
        
        
    }

    private void AutorsClick()
    {
        play.gameObject.SetActive(false);
        autors.gameObject.SetActive(false);
        settings.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        n1.gameObject.SetActive(false);
        n2.gameObject.SetActive(false);
        n3.gameObject.SetActive(false);
        
        
    }


    public void OnDisable()
    {
        
    }
}
