using UnityEngine;
using System.Collections;

public class Save : MonoBehaviour {

	// Use this for initialization
	public void savePosition () {
        PlayerPrefs.SetFloat("PlayerX", transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", transform.position.z);

    }

    public void loadPosition()
    {
        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        float z = PlayerPrefs.GetFloat("PlayerZ");

        transform.position = new Vector3(x, y, z);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
