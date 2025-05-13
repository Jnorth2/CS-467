using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameManager GameManager;
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }
    public Vector2Int num_tiles; //x, y number of tiles in a matrix
    public Vector2 offset; //spacing between tiles (units are game units which are ?)
    public GameObject Bricks; //tile object
    private int brickCount;
    public bool split;
    public bool ai;

    public AudioClip victorySound;
    private AudioSource audioSourceVictory;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(brickCount);
    }
    private void Awake()
    {
        //Initialize members
        this.GameManager = FindAnyObjectByType<GameManager>();
        this.num_tiles = this.GameManager.num_tiles;
        this.offset = this.GameManager.offset;
        this.split = this.GameManager.split;
        this.ai = this.GameManager.ai;
        brickCount = 0;
        //Generate the Scene
        SceneManager.sceneLoaded += OnLevelLoaded;
        this.GameManager.OnLevelLoaded();
        this.audioSourceVictory = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Go to Game over Screen
    private void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    public void OnLevelLoaded(Scene Scene, LoadSceneMode mode)
    {
        this.ball = FindAnyObjectByType<Ball>();
        this.ball.minSpeed = this.GameManager.minBallVel;
        this.paddle = FindAnyObjectByType<Paddle>();

        this.paddle.transform.localScale = this.GameManager.padddleSacle;
        //Create Tiles in a Grid 
        for (int i = 0; i < num_tiles.x; i++)
        {
            for (int j = 0; j < num_tiles.y; j++)
            {
                GameObject new_tile = Instantiate(Bricks, transform);
                new_tile.transform.position = transform.position + new Vector3((float)((num_tiles.x - 1) * 0.5f - i) * offset.x, j * offset.y, 0);
                Tile tile_params = new_tile.GetComponent<Tile>();
                brickCount++;
                if (tile_params != null)
                {
                    tile_params.set_health(j + 1);
                }
                else
                {
                    Debug.Log("No tile object found!");
                }
            }
        }
    }

    //Go to the next level
    private void LevelComplete()
    {
        //increase difficulty
        if (this.GameManager.num_tiles.x < 9)
        {
            this.GameManager.num_tiles.x++;
        }
        if (this.GameManager.num_tiles.y < 5)
        {
            this.GameManager.num_tiles.y++;
        }
        if (this.GameManager.padddleSacle.x > 4.0f)
        {
            this.GameManager.padddleSacle.x *= 0.9f;
        }
        this.GameManager.minBallVel *= 1.2f;
        this.GameManager.level++;
        SceneManager.sceneLoaded -= OnLevelLoaded;
        SceneManager.LoadScene("Level1");
    }

    //Track the brick count
    public void update_brickcount()
    {
        brickCount--;
        if (brickCount == 0)
        {
            if (victorySound != null && audioSourceVictory != null)
            {
                audioSourceVictory.PlayOneShot(victorySound);
            }
            LevelComplete();
        }
    }



}
