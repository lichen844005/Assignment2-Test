using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public Vector3 startPos, moveDirection, currentPos;
    public CharacterController controller;
    private float speed = 1f;
    private int direction;
    private int[] pathways = new int[4];

	// Use this for initialization
	void Start () {
        startPos = currentPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Physics.Raycast(transform.position, fwd, 0.5f))
        {
            WallHitDirectionChange();
        }
        moveDirection = transform.TransformDirection(Vector3.forward) * speed;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void WallHitDirectionChange()
    {

        Vector3 fwd;
        int greatest = -1;
        for (int j = 0; j < 4; j++)
        {
            transform.Rotate(0, 90f, 0);
            fwd = transform.TransformDirection(Vector3.forward);
            if (!Physics.Raycast(transform.position, fwd, 0.5f))
            {
                transform.position += transform.forward;
                for (int i = 0; i < 4; i++)
                {
                    transform.Rotate(0, 90f, 0);
                    fwd = transform.TransformDirection(Vector3.forward);
                    if (!Physics.Raycast(transform.position, fwd, 0.5f))
                    {
                        pathways[j]++;
                    }
                }
                transform.position -= transform.forward;
            }

        }

        for (int i = 0; i < pathways.Length; i++)
        {
            if (greatest < pathways[i])
            {
                direction = i;
                greatest = pathways[i];
            }
            else if (greatest == pathways[i])
            {
                System.Random random = new System.Random();
                if (random.Next(0, 2) == 0)
                {
                    direction = i;
                }
            }
        }
        pathways = new int[4];
        transform.Rotate(new Vector3(0, (direction + 1) * 90f, 0));
    }
}
