using UnityEngine;

public class Ball : MonoBehaviour
{
    public new Rigidbody2D rigidbody {get; private set;}
    public float speed = 500f; // ball speed

    private void Awake()  // Get ball item
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start() // ball start with slight delayed start before moving
    {
        
        ResetBall();
    }

    public void ResetBall()
    {
        this.transform.position = Vector2.zero;
        this.rigidbody.velocity = Vector2.zero;
        CancelInvoke(); // stop any queued force applications
        Invoke(nameof(RandomeTrajectory), 1.5f);
    }

    private void RandomeTrajectory() // ball movmeent and speed
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        this.rigidbody.AddForce(force.normalized * this.speed);
    }
}   
