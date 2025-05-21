using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameManager GameManager;
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }

    public Vector2Int num_tiles;  // x, y number of tiles in a matrix
    public Vector2 offset;        // spacing between tiles
    public GameObject Bricks;     // tile prefab

    private int brickCount;
    public bool split;
    public bool ai;

    public AudioClip victorySound;
    private AudioSource audioSourceVictory;

    private void Awake()
    {
        // Initialize values from GameManager
        GameManager = FindAnyObjectByType<GameManager>();
        num_tiles = GameManager.num_tiles;
        offset = GameManager.offset;
        split = GameManager.split;
        ai = GameManager.ai;
        brickCount = 0;

        audioSourceVictory = GetComponent<AudioSource>();

        // Scene event hook
        SceneManager.sceneLoaded += OnLevelLoaded;

        // Manually invoke if already loaded
        GameManager.OnLevelLoaded();
    }

    private void Start()
    {
        Debug.Log($"[Level] Brick count at start: {brickCount}");
    }

    private void Update() { }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Additive);
    }

    public void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        
        ball = FindAnyObjectByType<Ball>();
        paddle = FindAnyObjectByType<Paddle>();

        if (ball != null)
        {
            ball.minSpeed = GameManager.minBallVel;
        }
        else
        {
            Debug.LogWarning("[Level] Ball not found in scene.");
        }

        if (paddle != null)
        {
            paddle.transform.localScale = GameManager.padddleSacle;
        }
        else
        {
            Debug.LogWarning("[Level] Paddle not found in scene.");
        }

        if (Bricks == null)
        {
            Debug.LogError("[Level] ERROR: 'Bricks' prefab is not assigned in the Inspector!");
            return;
        }

        // Instantiate bricks in grid
        for (int i = 0; i < num_tiles.x; i++)
        {
            for (int j = 0; j < num_tiles.y; j++)
            {
                GameObject new_tile = Instantiate(Bricks, transform);
                new_tile.transform.position = transform.position + new Vector3(((num_tiles.x - 1) * 0.5f - i) * offset.x, j * offset.y, 0);

                Tile tile_params = new_tile.GetComponent<Tile>();
                brickCount++;

                if (tile_params != null)
                {
                    tile_params.set_health(j + 1);
                }
                else
                {
                    Debug.LogWarning("[Level] No Tile script found on instantiated brick.");
                }
            }
        }
    }

    private void LevelComplete()
    {
        // Increase difficulty
        if (GameManager.num_tiles.x < 9)
        {
            GameManager.num_tiles.x++;
        }

        if (GameManager.num_tiles.y < 5)
        {
            GameManager.num_tiles.y++;
        }

        if (GameManager.padddleSacle.x > 4.0f)
        {
            GameManager.padddleSacle.x *= 0.9f;
        }

        GameManager.minBallVel *= 1.2f;
        GameManager.level++;

        SceneManager.sceneLoaded -= OnLevelLoaded;
        SceneManager.LoadScene("Level1");
    }

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
