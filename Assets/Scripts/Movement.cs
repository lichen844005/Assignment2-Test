﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 5f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -speed, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, speed, 0));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.left * speed);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Translate(Vector3.left * -speed);
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
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection = Vector3.zero;
    }
}
