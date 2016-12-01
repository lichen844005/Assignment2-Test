using UnityEngine;
using System.Collections;

public class PlayerSave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown (KeyCode.X))
        {
            GameController.gameController.Delete();
        }

        if (Input.GetButtonDown ("Save"))
        {
            GameObject enemy = GameObject.Find("Enemy");
            GameController.gameController.playerPositionX = transform.position.x;
            GameController.gameController.playerPositionY = transform.position.y;
            GameController.gameController.playerPositionZ = transform.position.z;
            GameController.gameController.highScore = ScoreManager.getScore();
            GameController.gameController.enemyPositionX = enemy.transform.position.x;
            GameController.gameController.enemyPositionY = enemy.transform.position.y;
            GameController.gameController.enemyPositionZ = enemy.transform.position.z;

            GameController.gameController.Save();
        }

        if (Input.GetButtonDown("Load"))
        {
            GameController.gameController.Load();
            GameObject enemy = GameObject.Find("Enemy");
            transform.position = new Vector3

                (
                GameController.gameController.playerPositionX,
                GameController.gameController.playerPositionY,
                GameController.gameController.playerPositionZ
                );
            enemy.transform.position = new Vector3(
                GameController.gameController.enemyPositionX,
                GameController.gameController.enemyPositionY,
                GameController.gameController.enemyPositionZ
                );
            ScoreManager.SetScore(GameController.gameController.highScore);
        }


    }
}
