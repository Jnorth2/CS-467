using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Ball ball { get; private set; }
    public Paddle paddle {get; private set; }
    public int score = 0;
    public int lives = 3;
    public int level = 1;
    public Vector2Int num_tiles; //x, y number of tiles in a matrix
    public Vector2 offset; //spacing between tiles (units are game units which are ?)
    public GameObject Bricks; //tile object
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
        //Create Tiles in a Grid 
        for (int i = 0; i < num_tiles.x; i++)
        {
            for (int j = 0; j < num_tiles.y; j++)
            {
                GameObject new_tile = Instantiate(Bricks, transform);
                new_tile.transform.position = transform.position + new Vector3((float)((num_tiles.x - 1) * 0.5f - i) * offset.x, j * offset.y, 0);
                Tile tile_params = new_tile.GetComponent<Tile>();
                if (tile_params != null)
                {
                    tile_params.set_health(j+1);
                }
                else
                {
                    Debug.Log("No tile object found!");
                }
            }
        }
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
