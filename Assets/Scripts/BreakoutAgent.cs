using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using System.Collections;

public class BreakoutAgent : Agent
{
    private GameManager gameManager;
    private Rigidbody2D paddleRb;
    private Rigidbody2D ballRb;

    public float moveSpeed = 10f;

    public override void OnEpisodeBegin()
    {
        StartCoroutine(WaitForGameObjectsThenBegin());
    }

    private IEnumerator WaitForGameObjectsThenBegin()
    {
        // Wait until gameManager, ball, and paddle exist
        while (gameManager == null || gameManager.ball == null || gameManager.paddle == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
            yield return new WaitForSeconds(0.2f);
        }

        paddleRb = gameManager.paddle.GetComponent<Rigidbody2D>();
        ballRb = gameManager.ball.GetComponent<Rigidbody2D>();

        gameManager.ResetLevel();
        //gameManager.Miss();
        gameManager.ball.Launch();
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        if (gameManager == null)
        {
            gameManager = FindAnyObjectByType<GameManager>();
        }

        if (gameManager == null || gameManager.ball == null || gameManager.paddle == null)
        {
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            sensor.AddObservation(0f);
            return;
        }

        if (ballRb == null)
        {
            ballRb = gameManager.ball.GetComponent<Rigidbody2D>();
        }

        if (paddleRb == null)
        {
            paddleRb = gameManager.paddle.GetComponent<Rigidbody2D>();
        }

        sensor.AddObservation(gameManager.paddle.transform.position.x / 10f);
        sensor.AddObservation(gameManager.ball.transform.position.x / 10f);
        sensor.AddObservation(gameManager.ball.transform.position.y / 10f);
        sensor.AddObservation(ballRb.velocity.x / 10f);
        sensor.AddObservation(ballRb.velocity.y / 10f);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (paddleRb == null) return;

        int move = actions.DiscreteActions[0]; // 0 = stay, 1 = left, 2 = right
        Vector2 direction = Vector2.zero;

        if (move == 1) direction = Vector2.left;
        else if (move == 2) direction = Vector2.right;
        // Debug.Log($"[BreakoutAgent] Received move: {move}, direction: {direction}");


        paddleRb.velocity = direction * moveSpeed;

        if (direction != Vector2.zero)
        {
            AddReward(0.01f);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.A)) discreteActionsOut[0] = 1;
        else if (Input.GetKey(KeyCode.D)) discreteActionsOut[0] = 2;
        else discreteActionsOut[0] = 0;
    }

    public void RewardBallHit()
    {
        AddReward(0.2f);
        // Debug.Log("BreakoutAgent Reward: Paddle hit (+0.2)");
    }

    public void RewardBrickHit()
    {
        AddReward(1.0f);
        // Debug.Log("BreakoutAgent Reward: Brick hit (+1.0)");
    }

    public void PenalizeMiss()
    {
        AddReward(-1.0f);
        // Debug.Log("BreakoutAgent Penalty: Missed ball (-1.0)");
        EndEpisode();
    }
}
