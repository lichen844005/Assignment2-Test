using UnityEngine;
using System.Collections;

public class Player1Control : MonoBehaviour
{

    public CharacterController controller;
    public Camera camera;
    public float speed = 10;
    public int score = 0;
    private Vector3 moveDirection = Vector3.zero, pos;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || Input.GetAxis("Vertical") > 0)
        {
            moveDirection = new Vector3(0, speed, 0);
            //transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey("s") || Input.GetAxis("Vertical") < 0)
        {
            moveDirection = new Vector3(0, -speed, 0);
            //transform.Translate(Vector3.up * -speed * Time.deltaTime);
        }
        
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).position;
            Vector3 p = camera.ScreenToWorldPoint(new Vector3(touchDeltaPosition.x, touchDeltaPosition.y, 15.23f));
            Debug.Log(touchDeltaPosition);
            if (transform.position.y > p.y)
            {
                moveDirection = new Vector3(0, -speed, 0);
            }
            else if (transform.position.y < p.y)
            {
                moveDirection = new Vector3(0, speed, 0);
            }
        }
        
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection = Vector3.zero;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
