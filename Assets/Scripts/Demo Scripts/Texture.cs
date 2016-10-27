using UnityEngine;
using System.Collections;

public class Texture : MonoBehaviour {
    public GameObject North;
    public GameObject East;
    public GameObject West;
    public GameObject South;
    public GameObject Plane;
    public Material[] material;
    Renderer rend;
    // Use this for initialization
    void Start() {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }

    // Update is called once per frame
    void Update() {
            rend.sharedMaterial = material[0];
          
        }
 } 
 