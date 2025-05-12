using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ball ball { get; private set; }
    public Paddle paddle { get; private set; }

    public Vector2Int num_tiles; // x = columns, y = rows
    public Vector2 offset;       // spacing between bricks
    public GameObject Bricks;    // brick prefab

    private int bricksRemaining = 0;

    private void Start()
    {
        Debug.Log("GameManager → Initializing training environment.");
        ball = FindAnyObjectByType<Ball>();
        paddle = FindAnyObjectByType<Paddle>();
        SpawnBricks();
        ResetLevel();
    }

    private void SpawnBricks()
    {
        bricksRemaining = 0;

        for (int i = 0; i < num_tiles.x; i++)
        {
            for (int j = 0; j < num_tiles.y; j++)
            {
                GameObject new_tile = Instantiate(Bricks, transform);
                new_tile.transform.position = transform.position + new Vector3(
                    ((num_tiles.x - 1) * 0.5f - i) * offset.x,
                    j * offset.y,
                    0
                );

                Tile tile_params = new_tile.GetComponent<Tile>();
                if (tile_params != null)
                {
                    tile_params.set_health(j + 1);
                    tile_params.manager = this; // 🔗 Link back to GameManager
                    bricksRemaining++;
                }
                else
                {
                    Debug.LogWarning("GameManager → Tile prefab is missing Tile.cs");
                }
            }
        }
    }

    public void BrickDestroyed()
    {
        bricksRemaining--;
        Debug.Log($"GameManager → Brick destroyed. Remaining: {bricksRemaining}");

        if (bricksRemaining <= 0)
        {
            Debug.Log("GameManager → All bricks cleared! Resetting.");
            ResetBricks();

            // Optional: End episode to teach full-clear as a win
            BreakoutAgent agent = FindAnyObjectByType<BreakoutAgent>();
            agent?.EndEpisode();
        }
    }

    private void ResetBricks()
    {
        ClearBricks();
        SpawnBricks();
    }

    private void ClearBricks()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Tile"))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void ResetLevel()
    {
        ball?.ResetBall();
        paddle?.ResetPaddle();
    }

    public void Miss()
    {
        Debug.Log("GameManager → Miss() called.");
        ResetLevel();
    }
}
