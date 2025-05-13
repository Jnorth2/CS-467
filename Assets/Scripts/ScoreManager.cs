using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;

    int score = 0;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {score}";
        }
        else
        {
            Debug.Log("Score Text not assigned");
        }
    }

    public void UpdateScore(int point)
    {
        score += point;
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {score}";
        }
    }
}
