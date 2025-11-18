using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    private float spawnRate = 1.0f;
    private bool isPaused = false;
    public int lives = 3;

    public List<GameObject> targets;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public Button restartButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && lives > 0 && !titleScreen.activeInHierarchy)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        else 
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    IEnumerator SpawnTarget()
    {
        while(lives>0)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int value)
    {
        lives += value;
        livesText.text = "Lives: " + lives;
        if (lives == 0) { GameOver(); }
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);
        titleScreen.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
