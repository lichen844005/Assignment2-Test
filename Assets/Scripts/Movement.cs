using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 1f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 StartPos, StartLooking;
    public GameObject Player;
    private Shader dayShader, nightShader;
    private bool nightDay = false;
   // public GameObject Rigidbody;
    // Use this for initialization
    void Start () {
        StartPos = transform.position;
        StartLooking = transform.forward;
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.A) || CrossPlatformInputManager.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(new Vector3(0, -speed * 2, 0));
        }
        if (Input.GetKey(KeyCode.D) || CrossPlatformInputManager.GetAxis("Horizontal") > 0)
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
        if (Input.GetKey(KeyCode.W) || CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            moveDirection = transform.TransformDirection(Vector3.forward) * speed;
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || CrossPlatformInputManager.GetAxis("Vertical") < 0)
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
                wall.GetComponent<BoxCollider>().enabled = !wall.GetComponent<BoxCollider>().enabled;
                enemy.setMoving(!enemy.getMoving());
            }
        }
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Home))
        {
            Player.transform.position = new Vector3(0, 0.44f, 0.25f);
        }
        
        if (Input.GetKeyDown(KeyCode.U))
        {
            Renderer rend;
            foreach (Transform wall in GameObject.Find("Maze").transform)
            {
                foreach(Transform wallsection in wall)
                {
                    rend = wallsection.GetComponent<Renderer>();
                    if (wallsection.name == "South" || wallsection.name == "East" || wallsection.name == "North" || wallsection.name == "West")
                    {
                        if (nightDay == false)
                        {
                            rend.material.SetFloat("_AmbientLighIntensity", 0.2f);
                        }
                        else
                        {
                            rend.material.SetFloat("_AmbientLighIntensity", 1.0f);
                        }
                    }
                }
            }
            rend = GameObject.Find("FloorModel").GetComponent<Renderer>();
            if (nightDay == false)
            {
                rend.material.SetFloat("_AmbientLighIntensity", 0.2f);
            }
            else
            {
                rend.material.SetFloat("_AmbientLighIntensity", 1.0f);
            }
            nightDay = !nightDay;
        }

   }
      
}
