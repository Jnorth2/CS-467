
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public new Rigidbody2D rigidbody { get; private set;} // paddle item
    public Vector2 direction {get; private set;} // moving left and right
    public float speed = 30f; // speed of paddle movement, there are other setting in unity to edit how the movement feels in Rigidbody settings

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
        if (this.direction != Vector2.zero) {
            this.rigidbody.AddForce(this.direction * this.speed);

        }
    }
}
