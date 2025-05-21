using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager gameManager;
    private bool launchedByAI = false;

    public new Rigidbody2D rigidbody { get; private set; }
    public float speed = 500f; // Ball launch force
    public float minSpeed = 9.4f; // Minimum allowed speed during play

    private float minBounceAngle = 0.3f; // Minimum bounce angle to avoid flat trajectories
    private bool waitToLaunch = true; // Flag for launch state

    public AudioClip bounceSound;
    private AudioSource audioSource;

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        ResetBall();

        if (gameManager != null && gameManager.ai)
        {
            launchedByAI = true;
            Invoke(nameof(Launch), 1.0f); // Launch with delay for AI mode
        }
    }

    private void Update()
    {
        if (!launchedByAI && gameManager != null && !gameManager.ai && Input.GetKeyDown(KeyCode.Space) && waitToLaunch)
        {
            Launch(); // Manual launch by player
        }
        if (launchedByAI && waitToLaunch)
        {
            Launch();
        }
    }

    public void Launch()
    {
        if (waitToLaunch)
        {
            CancelInvoke();
            Invoke(nameof(RandomTrajectory), 1.5f); // Actual force is applied after a short delay
            waitToLaunch = false;
        }
    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;

        waitToLaunch = true;
        
        launchedByAI = false;

        

        launchedByAI = (gameManager != null && gameManager.ai);
        Debug.Log(launchedByAI);
        Debug.Log(waitToLaunch);
    }

    private void RandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        do
        {
            force.x = Random.Range(-1f, 1f);
            force.y = -1f;
        } while (Mathf.Abs(force.x) < minBounceAngle); // Prevent too vertical

        this.rigidbody.AddForce(force.normalized * this.speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Play bounce sound if set
        if (bounceSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }

        Vector2 v = this.rigidbody.velocity.normalized;

        // Enforce minimum bounce angle
        if (Mathf.Abs(v.x) < minBounceAngle)
        {
            v.x = Mathf.Sign(v.x) * minBounceAngle;
        }
        if (Mathf.Abs(v.y) < minBounceAngle)
        {
            v.y = Mathf.Sign(v.y) * minBounceAngle;
        }

        // Preserve speed after collision
        this.rigidbody.velocity = v.normalized * this.rigidbody.velocity.magnitude;

        // Enforce minimum speed after collision
        EnsureMinSpeed();

        // Log speed to console
        float currentSpeed = this.rigidbody.velocity.magnitude;
        Debug.Log($"Ball hit {collision.gameObject.name} | Speed: {currentSpeed:F2}");
    }

    private void FixedUpdate()
    {
        if (!waitToLaunch)
        {
            EnsureMinSpeed();
        }
    }

    private void EnsureMinSpeed()
    {
        float currentSpeed = this.rigidbody.velocity.magnitude;
        if (currentSpeed < minSpeed)
        {
            this.rigidbody.velocity = this.rigidbody.velocity.normalized * minSpeed;
        }
    }
}
