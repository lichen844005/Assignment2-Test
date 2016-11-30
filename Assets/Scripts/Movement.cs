using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Movement : MonoBehaviour {
    public float speed = 2f;
    public CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 StartPos;
    public GameObject Player;
    private Shader dayShader, nightShader;
    private bool nightDay = false;
    public AudioSource walking, wallHit;
    public AudioSource day, night;
    private float height;
    private bool switchFog = false;
    // public GameObject Rigidbody;

    // Use this for initialization
    void Start () {
        StartPos = transform.position;
        height = transform.position.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0 || CrossPlatformInputManager.GetAxis("Horizontal") < 0)
        {
            moveDirection += transform.TransformDirection(Vector3.left) * speed;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0 || CrossPlatformInputManager.GetAxis("Horizontal") > 0)
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
        if (moveDirection != Vector3.zero)
        {
            controller.Move(moveDirection * Time.deltaTime);
            moveDirection = Vector3.zero;
            Vector3 temp = new Vector3(transform.position.x, height, transform.position.z);
            transform.position = temp;
            if (!walking.isPlaying)
            {
                walking.Play();
            }
        } else
        {
            walking.Pause();
        }
        if (Input.GetKey(KeyCode.Home) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            transform.position = StartPos;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Joystick1Button6))
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
        
        if (Input.GetKeyDown(KeyCode.U) || Input.GetKeyDown(KeyCode.Joystick1Button3))
        {
            Renderer rend;
            foreach (Transform wall in GameObject.Find("Maze").transform)
            {
                foreach(Transform wallsection in wall)
                {
                    rend = wallsection.GetComponent<Renderer>();
                    if (wallsection.name == "South" || wallsection.name == "East" || wallsection.name == "North" || wallsection.name == "West" || wallsection.name == "Door")
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
            if (day.isPlaying)
            {
                day.Pause();
                night.Play();
            } else
            {
                night.Pause();
                day.Play();
            }
            nightDay = !nightDay;
        }
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Renderer rend;
            Shader shade1 = Shader.Find("Custom/NewShader");
            Shader shade2 = Shader.Find("Custom/FogShader");
            foreach (Transform wall in GameObject.Find("Maze").transform)
            {
                foreach (Transform wallsection in wall)
                {
                    if (wallsection.name == "South" || wallsection.name == "East" || wallsection.name == "North" || wallsection.name == "West" || wallsection.name == "Door")
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
            if (day.isPlaying)
            {
                day.volume = 0.05f;
            }
            else
            {
                night.volume = 0.05f;
            }
            if (switchFog)
            {
                day.volume = 0.1f;
                night.volume = 0.1f;
                switchFog = false;
            } else
            {
                switchFog = true;
            }
        }
        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.name == "Door")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }
        if (hit.collider.name == "South" || hit.collider.name == "East" || hit.collider.name == "North" || hit.collider.name == "West")
        {
            if (!wallHit.isPlaying)
            {
                wallHit.Play();
            }
        }
    }
}
