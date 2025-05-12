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
    }
}
