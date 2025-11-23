using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public GameObject gameOverUI;
    public bool isGameOver = false;

    private int count = 0;

    private void Start()
    {
        count = 0;
    }

    public void AddScore()
    {
        count+=1;
        counterText.text = "Count: " + count;
    }

    public void GameOver()
    {
        if (count != 0)
        {
            gameOverUI.SetActive(true);
            isGameOver = true;
        }
        else AddScore();
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
