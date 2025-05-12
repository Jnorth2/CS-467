using UnityEngine;

public class Missed : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.name == "Ball")
        {
            BreakoutAgent agent = FindAnyObjectByType<BreakoutAgent>();
            if (agent != null)
            {
                agent.PenalizeMiss();
            }
        }
    }
}