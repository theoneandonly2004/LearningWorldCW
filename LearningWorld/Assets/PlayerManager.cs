/*using UnityEngine;
using System.Collections;
using System.IO;

public class PlayerManager : MonoBehaviour {
    public static PlayerManager instance;  // this is a singleton class - accessible everywhere and in every scene

    string fileName = "playerData";
    public PlayerData playerData;
    int lastBallcount;

    void Awake() {
        if (!instance) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        playerData = new PlayerData();

        StreamReader reader = new StreamReader(Application.dataPath + "//" + fileName + ".json");
        string json = reader.ReadToEnd();
        playerData = JsonUtility.FromJson<PlayerData>(json);
        Debug.Log("Balls Collected on open: " + playerData.ballsCollected.ToString());
        print(json);
        reader.Close();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerData.ballsCollected != lastBallcount) {
            lastBallcount = playerData.ballsCollected;
            print("Ball collected" + playerData.ballsCollected);

            StreamWriter writer = new StreamWriter(Application.dataPath + "//" + fileName + ".json");
            string json = JsonUtility.ToJson(playerData);
            print(json);
            writer.Write(json);
            writer.Close();
        }
	}
}

[System.Serializable]
public class PlayerData {
    public string name;
    public int ballsCollected;
    public int score;
    public int level;
}
*/