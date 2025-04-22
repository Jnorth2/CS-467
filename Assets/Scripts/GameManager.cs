using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Ball ball { get; private set; }
    public Paddle paddle {get; private set; }
    public int score = 0;
    public int lives = 3;
    public int level = 1;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // will include struct for lives, points, levels

        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        this.score = 0;
        this.lives = 3;

        LoadLevel(1);
    }

    private void LoadLevel( int level)
    {
        this.level = level;
        SceneManager.LoadScene("Level" + level);
    }

    private void OnLevelLoaded(Scene Scene, LoadSceneMode mode)
    {
        this.ball = FindAnyObjectByType<Ball>();
        this.paddle = FindAnyObjectByType<Paddle>();
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

    public void Miss(){
       ResetLevel();
    }

}
