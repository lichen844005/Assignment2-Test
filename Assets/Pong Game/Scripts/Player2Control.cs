using UnityEngine;
using System.Collections;

public class Player2Control : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10;
    public int score = 0;
    private Vector3 moveDirection = Vector3.zero;
    public BallScript ball;

    // Update is called once per frame
    void Update()
    {
        if (LevelManager.val)
        {
            if (Input.GetKey("up"))
            {
                moveDirection = new Vector3(0, speed, 0);
                //transform.Translate(Vector3.up * speed * Time.deltaTime);
            }
            else if (Input.GetKey("down"))
            {
                moveDirection = new Vector3(0, -speed, 0);
                //transform.Translate(Vector3.up * -speed * Time.deltaTime);
            }
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection = Vector3.zero;
            controller.Move(moveDirection * Time.deltaTime);
        } else
        {
            
            if (ball.rb.velocity.x > 0)
            {
                if (ball.rb.velocity.normalized.y != 0 && transform.position.y - (transform.localScale.y / 2) > ball.transform.position.y)
                {
                    moveDirection = new Vector3(0, -speed, 0);
                } else if (ball.rb.velocity.normalized.y != 0 && transform.position.y + (transform.localScale.y / 2) < ball.transform.position.y)
                {
                    moveDirection = new Vector3(0, speed, 0);
                }
                controller.Move(moveDirection * Time.deltaTime);
                moveDirection = Vector3.zero;
                controller.Move(moveDirection * Time.deltaTime);
            }
            
        }
    }
}
