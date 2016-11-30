using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Enemy")
            Destroy(gameObject);

    }
    
}