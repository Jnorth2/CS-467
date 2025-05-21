using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Level Level;
    public GameManager GameManager;
    private int block_health = 3;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        this.GameManager = FindAnyObjectByType<GameManager>();
        this.Level = FindAnyObjectByType<Level>();
        sr = GetComponent<SpriteRenderer>();
        update_color();

    }

    public void set_health(int health)
    {
        block_health = health;
        update_color();
    }

    void collision_occured()
    {
        block_health -= 1;
        if (block_health <= 0)
        {
            Destroy(gameObject);
            this.Level.update_brickcount();
            this.GameManager.UpdateScore(5);
            // update UI
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.UpdateScore(5);
            }
        }
        else
        {
            update_color();
            this.GameManager.UpdateScore(1);
            ScoreManager.instance.UpdateScore(1);
        }
    }
    
    void update_color()
    {
        if (sr == null)
        {
            Debug.Log("No sr");
            return;
        }
        
        switch (block_health)
        {
            case 1:
                sr.color = Color.green;
                break;
            case 2:
                sr.color = Color.blue;
                break;
            case 3:
                sr.color = Color.yellow;
                break;
            case 4:
                Color orange = new Color(1f, 0.5f, 0f);
                sr.color = orange;
                break;
            default:
                sr.color = Color.red;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Ball")
        {
            collision_occured();
        }
    }
}
