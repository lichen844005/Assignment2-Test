using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public int scoreToAdd = 1;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name != "bullet(Clone)")
        {
            GetComponent<AudioSource>().Play();
        }
        if (collision.collider.name == "Enemy")
        {
            ScoreManager.AddScore(scoreToAdd);
            Destroy(gameObject);
        }

    }
}
