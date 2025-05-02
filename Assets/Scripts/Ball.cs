using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody {get; private set;}
    public float speed = 500f; // ball speed
    private float minBounceAngle = 0.3f; // minimum angle the ball bounces
    private bool waitToLaunch = true; // flag to check whether ball in start/reset position

    public AudioClip bounceSound;
    private AudioSource audioSource;

    private void Awake()  // Get ball item
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.audioSource = GetComponent<AudioSource>();
    }

    private void Start() // ball start with slight delayed start before moving
    {
        
        ResetBall();
    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;
        waitToLaunch = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && waitToLaunch) // press Spacebar to launch ball from starting/reset position
        {
            CancelInvoke(); // stop any queued force applications
            Invoke(nameof(RandomeTrajectory), 1.5f);
            waitToLaunch = false;
        }
    }

    private void RandomeTrajectory() // ball movmeent and speed
    {
        Vector2 force = Vector2.zero;
        do
        {
            force.x = Random.Range(-1f, 1f);
            force.y = -1f;
        } while (Mathf.Abs(force.x) < minBounceAngle);  // prevent too vertical

        this.rigidbody.AddForce(force.normalized * this.speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // play bounce sound
        if (bounceSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(bounceSound);
        }

        Vector2 v = this.rigidbody.velocity.normalized;

        // enforce minimum bounce angle; prevents ball from being "stuck" bouncing horizontally or vertically
        if (Mathf.Abs(v.x) < minBounceAngle)
        {
            v.x = Mathf.Sign(v.x) * minBounceAngle;
        }
        if (Mathf.Abs(v.y) < minBounceAngle)
        {
            v.y = Mathf.Sign(v.y) * minBounceAngle;
        }

        // preserve speed
        this.rigidbody.velocity = v.normalized * this.rigidbody.velocity.magnitude;
    }
}   
