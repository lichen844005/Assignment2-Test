using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {


    public AudioSource Hitting;
    public AudioSource GettingHit;
    public AudioSource HittingWalls;


    private static bool sfxmanexists;

    // Use this for initialization
    void Start() {

        if (!sfxmanexists)
        {
            sfxmanexists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
	
	