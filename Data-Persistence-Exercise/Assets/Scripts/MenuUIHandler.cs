using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.Instance.HighScore != 0)
        {
            bestScoreText.text = "Best Score : " + DataManager.Instance.HighScorePlayer + " : " + DataManager.Instance.HighScore;
        }
    }

}
