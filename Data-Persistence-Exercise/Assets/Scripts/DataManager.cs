using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string PlayerName;
    public int HighScore;
    public string HighScorePlayer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.SetString("HighScorePlayer", HighScorePlayer);
        PlayerPrefs.Save();
    }

    public void LoadData() 
    {
        HighScorePlayer = PlayerPrefs.GetString("HighScorePlayer", "");
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void SetPlayerName(string name)
    {
        Debug.Log("Setting player name to: " + name);
        PlayerName = name;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        SaveData();
    }
}
