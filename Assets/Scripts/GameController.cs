using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

    public static GameController gameController;

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;
    public float enemyPositionX;
    public float enemyPositionY;
    public float enemyPositionZ;
    public int highScore = 0;
    public Font hsFont;

    void Awake()
    {
        if (gameController == null)
        {
            DontDestroyOnLoad(gameObject);
            gameController = this;
            hsFont = Font.CreateDynamicFontFromOSFont("Arial", 24);

        }
        else if (gameController != this)

        {
            Load();
            Destroy(gameObject);
        }
    }


    public void Save()

    {

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.OpenOrCreate);

        PlayerData data = new PlayerData();
        data.playerPosX = playerPositionX;
        data.playerPosY = playerPositionY;
        data.playerPosZ = playerPositionZ;
        data.score = highScore;
        data.enemyPosX = enemyPositionX;
        data.enemyPosY = enemyPositionY;
        data.enemyPosZ = enemyPositionZ;

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadTheLevel(string theLevel)
    {
     //   SceneManager.LoadScene(which one?);
    }

    public void Load ()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open, FileAccess.Read);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerPositionX = data.playerPosX;
            playerPositionY = data.playerPosY;
            playerPositionZ = data.playerPosZ;
            highScore = data.score;
            enemyPositionX = data.enemyPosX;
            enemyPositionY = data.enemyPosY;
            enemyPositionZ = data.enemyPosZ;
        }
    }

  

    public void Delete()
    {
        if (File.Exists (Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("File Deleted");
            File.Delete(Application.persistentDataPath + "/playerInfo.dat");
        }
    }

}

[Serializable]
class PlayerData
{
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public float enemyPosX;
    public float enemyPosY;
    public float enemyPosZ;
    public int score;

}