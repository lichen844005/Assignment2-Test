using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 5f;
    public Camera camera;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, speed, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, -speed, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Translate(Vector3.left * -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }
    }
}
