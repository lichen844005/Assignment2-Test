using UnityEngine;
using System.Collections;

public class Lookingupordown : MonoBehaviour {

    public float speed = 20f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.left, speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {

            transform.Rotate(Vector3.right, speed * Time.deltaTime);
        }
    }
}
