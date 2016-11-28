using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    public int scoreToAdd = 1;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.collider.name == "Enemy")
        {
            ScoreManager.AddScore(scoreToAdd);

            Destroy(gameObject);
        }

    }
}
