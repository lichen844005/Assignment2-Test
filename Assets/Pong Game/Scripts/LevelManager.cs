using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public static bool val = false;

	public void LoadGame1(string name)
    {
        val = false;
        SceneManager.LoadScene(name);
    }
    public void LoadGame2(string name)
    {
        val = true;
        SceneManager.LoadScene(name);
    }
}
