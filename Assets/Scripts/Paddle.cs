
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set;} // paddle item
    public Vector2 direction {get; private set;} // moving left and right
    public float speed = 30f; // speed of paddle movement, there are other setting in unity to edit how the movement feels in Rigidbody settings
    public float maxAngle = 75f;

    private void Awake() // initalize paddle
    {
        this.rigidbody = GetComponent<Rigidbody2D>();

    }

    public void ResetPaddle()  // paddle reset after missing the ball
    {
        this.transform.position = new Vector2(0f, this.transform.position.y);
        this.rigidbody.velocity = Vector2.zero;
    }

    private void Update() // update runs in unity at an interval and retrieves input status of movement
    {
        if (Input.GetKey(KeyCode.A)) {
            this.direction = Vector2.left;

        } else if (Input.GetKey(KeyCode.D)){
            this.direction = Vector2.right;

        } else {
            this.direction = Vector2.zero;
        }
    }

    private void FixedUpdate() // moves paddel 
    {
        if (this.direction != Vector2.zero) 
        {
            this.rigidbody.AddForce(this.direction * this.speed);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // Ball-paddle collision handling
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();

        if (ball != null)
        {
            Vector3 paddlePosition = this.transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float paddleOffset = contactPoint.x - paddlePosition.x;
            float paddleWidth = collision.otherCollider.bounds.size.x / 2f;
            float normalizedOffset = Mathf.Clamp(paddleOffset / paddleWidth, -1f, 1f);

            // Determine if ball is hitting from same side it's moving toward
            float incomingX = ball.rigidbody.velocity.x;
            float directionAgreement = Mathf.Sign(incomingX) * Mathf.Sign(normalizedOffset);

            // Apply a multiplier for stronger directional return if same side
            float effectMultiplier = (directionAgreement > 0) ? 1.0f : 0.3f;

            // Use a sine-based arch to calculate bounce angle
            float curvedOffset = Mathf.Sin(normalizedOffset * Mathf.PI / 2f); // Smooth arch from -1 to 1
            float returnAngle = -curvedOffset * this.maxAngle * effectMultiplier;

            // Prevent overly flat horizontal bounces
            if (Mathf.Abs(returnAngle) < 15f)
            {
                returnAngle = 15f * Mathf.Sign(returnAngle);
            }

            Quaternion rotation = Quaternion.AngleAxis(returnAngle, Vector3.forward);
            ball.rigidbody.velocity = rotation * Vector2.up * ball.rigidbody.velocity.magnitude;
        }
    }
}
