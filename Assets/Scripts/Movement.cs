using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 1f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 StartPos, StartLooking;
    // Use this for initialization
    void Start () {
        StartPos = transform.position;
        StartLooking = transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -speed * 2, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, speed * 2, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            moveDirection = transform.TransformDirection(Vector3.left) * speed;
        }
        if (Input.GetKey(KeyCode.E))
        {
            moveDirection = transform.TransformDirection(Vector3.left) * -speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = transform.TransformDirection(Vector3.forward) * speed;
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection = transform.TransformDirection(Vector3.forward) * -speed;
            //transform.Translate(Vector3.forward * -speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Home))
        {
            transform.position = StartPos;
            transform.forward = StartLooking;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W))
        {
            GameObject maze = GameObject.FindGameObjectWithTag("Maze");
            foreach (Transform wall in maze.transform)
            {
                wall.GetComponent<BoxCollider>().enabled = !wall.GetComponent<BoxCollider>().enabled;
            }
        }
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection = Vector3.zero;
    }
}
