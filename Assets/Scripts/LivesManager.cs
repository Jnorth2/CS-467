using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;
    public TMP_Text livesText;
    public GameManager GameManager;

    int lives = 3;

    void Awake()
    {
        this.GameManager = FindAnyObjectByType<GameManager>();
        instance = this;
        //DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        if (livesText != null)
        {
            livesText.text = $"LIVES: {GameManager.lives}";
        }
        else
        {
            Debug.Log("Lives Text not assigned");
        }
    }

    public void UpdateLives()
    {
        lives -= 1;
        if (livesText != null)
        {
            livesText.text = $"LIVES: {GameManager.lives}";
        }
    }
}
