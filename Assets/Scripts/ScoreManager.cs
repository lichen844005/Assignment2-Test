using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    private GameObject Text;
    private static int score;
    private static Text text;

	// Use this for initialization
	void Awake ()
    {
        text = GetComponent<Text>();
        score = 0;
	}

    public static void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        text.text = "Score:" + score;
    }
}
