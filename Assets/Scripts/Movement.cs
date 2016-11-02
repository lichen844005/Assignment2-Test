using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 1f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 StartPos, StartLooking;
    public GameObject Player;
   // public GameObject Rigidbody;
    // Use this for initialization
    void Start () {
        StartPos = transform.position;
        StartLooking = transform.forward;
	}

    // Update is called once per frame
    void Update() {
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
            GameObject maze = GameObject.Find("Maze");
            EnemyScript enemy = GameObject.Find("Enemy").GetComponent<EnemyScript>();
            foreach (Transform wall in maze.transform)
            {
                foreach (Transform wallSection in wall.transform)
                {
                    wallSection.GetComponent<MeshCollider>().enabled = !wallSection.GetComponent<MeshCollider>().enabled;
                }
            }
            enemy.setMoving(!enemy.getMoving());
            if (enemy.GetComponent<Animation>().isPlaying)
            {
                enemy.GetComponent<Animation>().Stop();
            } else
            {
                enemy.GetComponent<Animation>().Play("Take 001");
            }
            
        }
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Home))
        {
            Player.transform.position = new Vector3(0, 0.44f, 0.25f);
        }
       

       }

//    void RemoveRigidbody()
    //{

       // if (this.gameObject.gameObject.< Rigidbody > ())
         //   Destroy(this.gameObject.gameObject.< Rigidbody > ());

        
  //if (Input.GetKeyDown(KeyCode.P))
       // {

           // RemoveRigidbody();
        //}
    //}
    
      
}
