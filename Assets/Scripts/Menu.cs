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
        //Set game parameters
        if (type == 0)
        {
            this.GameManager.ai = true;
        }
        else if (type == 1)
        {
            this.GameManager.split = true;
        }
        this.GameManager.num_tiles.x = 2;
        this.GameManager.num_tiles.y = 1;
        this.GameManager.offset.x = 4;
        this.GameManager.offset.y = 1.5f;
        this.GameManager.level = 1;
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}
