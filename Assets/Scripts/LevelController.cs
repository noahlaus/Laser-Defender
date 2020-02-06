﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] float deathDelay = 2f;
    public void LoadStartMenu(){
        SceneManager.LoadScene(0);
    }

    public void LoadGame(){
        SceneManager.LoadScene(1);
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver(){
        StartCoroutine(WaitAndLoad());
    }

    IEnumerator WaitAndLoad(){
        yield return new WaitForSeconds(deathDelay);
        SceneManager.LoadScene(2);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
