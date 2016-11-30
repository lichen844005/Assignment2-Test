using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WinManager : MonoBehaviour
{

    private GameObject gameobj;
    public GlobalScript global;

    void Awake()
    {
        gameobj = GameObject.FindGameObjectsWithTag("Global")[0] as GameObject;
        global = gameobj.GetComponent<GlobalScript>();
        GameObject GameText = GameObject.Find("WinnerText");
        Text text = GameText.GetComponent<Text>();
        text.text = global.getWinner();
    }

    public void LoadMenu(string name)
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(name);
    }

}
