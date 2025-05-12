using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Ball ball { get; private set; }
    public float minBallVel;
    public Paddle paddle {get; private set; }
    public Vector3 padddleSacle;
    public int score = 0;
    public int lives = 3;
    public int level = 1;
    public Vector2Int num_tiles; //x, y number of tiles in a matrix
    public Vector2 offset; //spacing between tiles (units are game units which are ?)
    public Vector3 scale; //Size of bricks
    public GameObject Bricks; //tile object
    public bool split;
    public bool ai;
    private void Awake()
    {
        this.minBallVel = 9.4f;
        this.padddleSacle = new Vector3(5.7f, 0.5f, 1.0f);
        this.split = false;
        this.ai = false;
        this.scale = new Vector3(3, 1, 1);

        DontDestroyOnLoad(this.gameObject); // will include struct for lives, points, levels

        //SceneManager.sceneLoaded += OnLevelLoaded;
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
        this.num_tiles.x = 2;
        this.num_tiles.y = 1;
        this.offset.x = 4;
        this.offset.y = 1.5f;
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

    public void OnLevelLoaded()
    {
        this.ball = FindAnyObjectByType<Ball>();
        this.paddle = FindAnyObjectByType<Paddle>();
        //Create Tiles in a Grid 
        //for (int i = 0; i < num_tiles.x; i++)
        //{
        //    for (int j = 0; j < num_tiles.y; j++)
        //    {
        //        GameObject new_tile = Instantiate(Bricks, transform);
        //        new_tile.transform.position = transform.position + new Vector3((float)((num_tiles.x - 1) * 0.5f - i) * offset.x, j * offset.y, 0);
        //        Tile tile_params = new_tile.GetComponent<Tile>();
        //        if (tile_params != null)
        //        {
        //            tile_params.set_health(j+1);
        //        }
        //        else
        //        {
        //            Debug.Log("No tile object found!");
        //        }
        //    }
        //}
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

    public void Miss(){
        this.lives -= 1;
        if (this.lives <= 0)
        {
            SceneManager.sceneLoaded -= FindAnyObjectByType<Level>().OnLevelLoaded;
            GameOver();

        }
        ResetLevel();
    }

}
