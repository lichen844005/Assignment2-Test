using UnityEngine;
using System.Collections;

public class GlobalScript : MonoBehaviour {

    private string winner;

    public void setWinner(string win)
    {
        winner = win;
    }

    public string getWinner()
    {
        return winner;
    }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
