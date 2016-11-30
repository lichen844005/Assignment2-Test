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
            GameController.gameController.playerPositionX = transform.position.x;
            GameController.gameController.playerPositionY = transform.position.y;
            GameController.gameController.playerPositionZ = transform.position.z;
        }

        if (Input.GetButtonDown("Load"))
        {
            GameController.gameController.Load();

            transform.position = new Vector3

                (
                GameController.gameController.playerPositionX,
                GameController.gameController.playerPositionY,
                GameController.gameController.playerPositionZ
                );
        }


    }
}
