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
    [SerializeField] private Button play, autors, exit, back;
    [SerializeField] private GameObject n1, n2, autor, board;
    
    public void OnEnable()
    {   
        play.onClick.AddListener(() => SceneManager.LoadScene("Game"));
        exit.onClick.AddListener(Application.Quit);
        autors.onClick.AddListener(AutorsClick);
        back.onClick.AddListener(BackClick);
    }

    private void BackClick()
    {
        play.gameObject.SetActive(true);
        autors.gameObject.SetActive(true);
        exit.gameObject.SetActive(true);
        n1.gameObject.SetActive(true);
        n2.gameObject.SetActive(true);
        board.gameObject.SetActive(true);
        
        autor.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }

    private void AutorsClick()
    {
        play.gameObject.SetActive(false);
        autors.gameObject.SetActive(false);
        exit.gameObject.SetActive(false);
        n1.gameObject.SetActive(false);
        n2.gameObject.SetActive(false);
        board.gameObject.SetActive(false);
        
        autor.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
    }


    public void OnDisable()
    {
        
    }
}
