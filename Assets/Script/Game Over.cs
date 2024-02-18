using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Button menu, restart;

    private void OnEnable()
    {
        menu.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        restart.onClick.AddListener(() => SceneManager.LoadScene("Game"));
    }
}
