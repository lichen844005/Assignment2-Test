using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

    public float horzontialSpeed = 2.0f;
    public float verticalSpeed = 2.0f;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public int damagePershoot = 20;
    private float pitch;
    private float yaw;
    public GameObject Enemy;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            yaw += horzontialSpeed * Input.GetAxis("Mouse X");
            pitch -= verticalSpeed * Input.GetAxis("Mouse Y");
        } else
        {
            yaw += horzontialSpeed * Input.GetAxis("RightStickX");
            pitch -= verticalSpeed * -Input.GetAxis("RightStickY");
        }
        

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Fire();
        }
        
    }
    // 我需要一个子弹的code
    void Fire()
    {
        var bullet = (GameObject)Instantiate( bulletPrefab,
             bulletSpawn.position,
             bulletSpawn.rotation);


        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
        Destroy(bullet, 2.0f);
    }
   
}
