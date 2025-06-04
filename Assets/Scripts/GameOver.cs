using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        this.GameManager = FindAnyObjectByType<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GotoMenu()
    {
        Debug.Log("[GameOver] Menu button clicked");

        if (this.GameManager != null)
        {
            // Debug.Log($"[GameOver] GameManager.ai = {this.GameManager.ai}");
            // Debug.Log($"[GameOver] GameManager.split = {this.GameManager.split}");
            // Debug.Log($"[GameOver] GameManager.lives = {this.GameManager.lives}");
            // Debug.Log($"[GameOver] GameManager.score = {this.GameManager.score}");
            // Debug.Log($"[GameOver] GameManager.level = {this.GameManager.level}");
            this.GameManager.ResetGame();
        }
        else
        {
            // Debug.LogWarning("[GameOver] GameManager is null in GotoMenu");
        }

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void PlayAgain()
    {
        bool ai = this.GameManager.ai;
        bool split = this.GameManager.split;
        this.GameManager.ResetGame();
        this.GameManager.ai = ai;
        this.GameManager.split = split;
        //remove level load to regenerate
        //SceneManager.sceneLoaded -= FindAnyObjectByType<Level>().OnLevelLoaded;
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
        //Debug.Log("[GameOver] PlayAgain called");
        //Debug.Log($"[GameOver] Before ResetGame - GameManager.lives = {GameManager.lives}");
    }
}
