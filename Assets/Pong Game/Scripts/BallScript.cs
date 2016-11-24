using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BallScript : MonoBehaviour {

    public Rigidbody rb;
    public Player2Control player2;
    public Player1Control player1;
    public Text text, text2;
    private int speed = 15;
    private int winScore = 5;
    public GlobalScript play;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(25, 0, 0);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Player")
        {
            rb.velocity = rb.velocity.normalized * speed;
            float num = (col.collider.transform.position.y + (col.collider.bounds.size.y / 2) - transform.position.y);
            float Normalnum = (num / (col.collider.bounds.size.y / 2));
            if (Normalnum > 1.2)
            {
                rb.velocity = new Vector3(rb.velocity.x, -10, rb.velocity.z);
            } else if (Normalnum < 0.8) {
                rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
            }
            speed++;
        }
        if (col.collider.gameObject.name == "Player 1 Wall")
        {
            player2.score++;
            text.text = "Player 1: " + player2.score;
            if (player2.score == winScore)
            {
                
                play.setWinner("Player 1 wins");
                SceneManager.LoadScene("Win Scene");
            }
            Respawn();
        } else if (col.collider.gameObject.name == "Player 2 Wall")
        {
            player1.score++;
            text2.text = "Player 2: " + player1.score;
            if (player1.score == winScore)
            {
                play.setWinner("Player 2 wins");
                SceneManager.LoadScene("Win Scene");
            }
            Respawn();
        }
    }

    void Respawn()
    {
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, 0, 5);
        System.Random rand = new System.Random();
        int num = 90, num2 = rand.Next(0, 90);
        speed = 15;
        switch (rand.Next(0, 4)) {
            case 0:
                rb.AddForce(new Vector2(num, num2));
                break;
            case 1:
                rb.AddForce(new Vector2(num, -num2));
                break;
            case 2:
                rb.AddForce(new Vector2(-num, num2));
                break;
            case 3:
                rb.AddForce(new Vector2(-num, -num2));
                break;
        }
    }
}
