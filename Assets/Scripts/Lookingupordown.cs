using UnityEngine;
using System.Collections;

public class Lookingupordown : MonoBehaviour {

    public float speed = 1f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(new Vector3(-speed, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(new Vector3(speed, 0, 0));
        }
    }
}
