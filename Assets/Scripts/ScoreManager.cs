using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    public GameManager GameManager;

    int score = 0;

    private void Awake()
    {
        this.GameManager = FindAnyObjectByType<GameManager>();
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {GameManager.score}";
        }
        else
        {
            Debug.Log("Score Text not assigned");
        }
    }

    public void UpdateScore(int point)
    {
        score += point;
        Debug.Log(GameManager.score);
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {GameManager.score}";
        }
    }
}
