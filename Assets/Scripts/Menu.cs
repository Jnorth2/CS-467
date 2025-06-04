using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameManager GameManager;
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

    //Quit the application (only works in build mode)
    public void quit()
    {
        Application.Quit();
    }

    //Load the first Level
    public void LoadLevel(int type)
    {
        Debug.Log($"[Menu] LoadLevel type: {type}, GameManager.lives = {GameManager.lives}");
        //Set game parameters
        if (type == 0)
        {
            Debug.Log("[Menu] ML Play selected");
            Debug.Log($"[DEBUG - ML Play] GameManager.lives = {GameManager.lives}");
            this.GameManager.ai = true;
            this.GameManager.lives = 3;
        }
        else if (type == 1)
        {
            Debug.Log("[Menu] Single Player selected");
            this.GameManager.ai = false;
            // this.GameManager.split = true;
            Debug.Log($"[Menu] LoadLevel Summary — ai: {GameManager.ai}, split: {GameManager.split}");
        }
        this.GameManager.num_tiles.x = 2;
        this.GameManager.num_tiles.y = 1;
        this.GameManager.offset.x = 4;
        this.GameManager.offset.y = 1.5f;
        this.GameManager.level = 1;
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
