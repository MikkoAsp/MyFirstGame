﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level");
    }
    public void QuitGame()
    {
        Debug.Log("Game quitted!");
        Application.Quit();
    }
}
