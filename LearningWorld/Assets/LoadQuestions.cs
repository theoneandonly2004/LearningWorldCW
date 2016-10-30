using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LoadQuestions : MonoBehaviour {

    string[] topicNames = { "WebDev", "GamesDevelopment", "WebDataBases", "SoftwareDevelopment", "ImageProcessing", "DataAnalytics" };
    public Questions[] quizes;
    string fileName = "Questions";
    Questions quiz;

    // Use this for initialization
    void Start () {
        StreamReader reader;
        string json;
        quizes = new Questions[topicNames.Length];
        //quiz = new Questions();

        for(int count = 0; count < topicNames.Length; count++)
        {
            fileName = topicNames[count];
            reader = new StreamReader(Application.dataPath + "//Questions//" + fileName + ".json");
            json = reader.ReadToEnd();
            quizes[count] = JsonUtility.FromJson<Questions>(json);

        }
      

        
    }
	
	// Update is called once per frame
	void Update () {
        
	}

   public Questions getQuestionByName(string name)
    {
        bool isFound = false;
        int count = 0;

        while (count < topicNames.Length)
        {
            if(topicNames[count] == name)
            {
                return quizes[count];
            }
            count++;                        
            
        }
        return null;
    }
}

[System.Serializable]
public class Questions {

    public int num_questions;
    public int num_answers;
    public Answers[] answers; 
    
}

[System.Serializable]
public class Answers {

    public string question;
    public string answer;
    public string option1;
    public string option2;

}

