using UnityEngine;

public class Missed : MonoBehaviour
{
    public AudioClip missedSound;
    private AudioSource audioSourceMiss;

    private void Awake()
    {
        this.audioSourceMiss = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        if (collision.gameObject.name == "Ball")
        {
            Debug.Log("Ball hit the bottom — calling Miss()");
            FindObjectOfType<GameManager>().Miss();

            if (missedSound != null & audioSourceMiss != null)
            {
                audioSourceMiss.PlayOneShot(missedSound);
            }
        }
    }
}