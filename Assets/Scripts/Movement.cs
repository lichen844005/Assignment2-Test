using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class Movement : MonoBehaviour {
    public float speed = 2f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 StartPos, StartLooking;
    public GameObject Player;
    private Shader dayShader, nightShader;
    private bool nightDay = false;
    public float gravity = 2.0f;
   // public GameObject Rigidbody;
    // Use this for initialization
    void Start () {
        StartPos = transform.position;
        StartLooking = transform.forward;
	}

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Q) || Input.GetAxis("RightStickX") < 0 || CrossPlatformInputManager.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(new Vector3(0, -speed * 2, 0));
        }
        if (Input.GetKey(KeyCode.E) || Input.GetAxis("RightStickX") > 0 || CrossPlatformInputManager.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(new Vector3(0, speed * 2, 0));
        }
        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0)
        {
            moveDirection += transform.TransformDirection(Vector3.left) * speed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
        {
            moveDirection += transform.TransformDirection(Vector3.left) * -speed;
        }
        if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") > 0 || CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            moveDirection += transform.TransformDirection(Vector3.forward) * speed;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") < 0 || CrossPlatformInputManager.GetAxis("Vertical") < 0)
        {
            moveDirection += transform.TransformDirection(Vector3.forward) * -speed;
        }
        if (Input.GetKey(KeyCode.Home) || Input.GetKeyDown(KeyCode.Joystick1Button7))
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

        moveDirection.y = -gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        moveDirection = Vector3.zero;
        
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
                            rend.material.SetFloat("_AmbientLighIntensity", 0.9f);
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
                rend.material.SetFloat("_AmbientLighIntensity", 0.9f);
            }
            nightDay = !nightDay;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Renderer rend;
            Shader shade1 = Shader.Find("Custom/NewShader");
            Shader shade2 = Shader.Find("Custom/FogShader");
            foreach (Transform wall in GameObject.Find("Maze").transform)
            {
                foreach (Transform wallsection in wall)
                {
                    if (wallsection.name == "South" || wallsection.name == "East" || wallsection.name == "North" || wallsection.name == "West")
                    {
                        rend = wallsection.GetComponent<Renderer>();
                        if (rend.material.shader.name == "Custom/NewShader")
                        {
                            rend.material.shader = shade2;
                        }
                        else
                        {
                            rend.material.shader = shade1;
                        }
                    }
                }
            }
            rend = GameObject.Find("FloorModel").GetComponent<Renderer>();
            if (rend.material.shader.name == "Custom/NewShader")
            {
                rend.material.shader = shade2;
            }
            else
            {
                rend.material.shader = shade1;
            }
        }

   }
      
}
