using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public GameObject breakoutAgentObject;
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }

    public float minBallVel;
    public Vector3 padddleSacle;
    public int score = 0;
    public int lives = 3;
    public int level = 1;

    public Vector2Int num_tiles;    // x, y number of tiles in a matrix
    public Vector2 offset;          // spacing between tiles
    public Vector3 scale;           // size of bricks
    public GameObject Bricks;       // tile object

    public bool split;
    public bool ai;

    private void Awake()
    {
        this.minBallVel = 9.4f;
        this.padddleSacle = new Vector3(5.7f, 0.5f, 1.0f);
        this.split = false;
        this.ai = false;
        this.scale = new Vector3(3, 1, 1);

        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;
        LoadLevel();
    }

    public void ResetGame()
    {
        this.minBallVel = 9.4f;
        this.padddleSacle = new Vector3(5.7f, 0.5f, 1.0f);
        this.split = false;
        this.ai = false;
        this.num_tiles = new Vector2Int(2, 1);
        this.offset = new Vector2(4f, 1.5f);
        this.level = 1;
        this.score = 0;
        this.lives = 3;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Menu");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Level"))
        {
            OnLevelLoaded();
        }
    }

    public void OnLevelLoaded()
    {
        this.ball = FindAnyObjectByType<Ball>();
        this.paddle = FindAnyObjectByType<Paddle>();

        if (paddle != null)
        {
            var paddleScript = paddle.GetComponent<Paddle>();
            if (paddleScript != null) paddleScript.enabled = !ai;
        }

        if (breakoutAgentObject != null)
            breakoutAgentObject.SetActive(ai);
    }

    public void UpdateScore(int new_score)
    {
        score += new_score;
    }

    private void ResetLevel()
    {
        if (this.ball != null)
        {
            this.ball.ResetBall();
        }

        if (this.paddle != null)
        {
            this.paddle.ResetPaddle();
        }
    }

    public void Miss()
    {
        this.lives -= 1;

        if (LivesManager.instance != null)
        {
            LivesManager.instance.UpdateLives();
        }

        if (this.lives <= 0)
        {
            SceneManager.sceneLoaded -= FindAnyObjectByType<Level>().OnLevelLoaded;
            GameOver();
        }

        ResetLevel();
    }
}
