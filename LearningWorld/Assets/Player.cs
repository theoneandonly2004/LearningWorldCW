using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class Player : MonoBehaviour {
       
    Dictionary<string, ClassStats> classes;
    Player currentPlayer;
    ClassStats currentReadStats;
    string fileName = "PlayerData";
    ClassStats[] statsArray = new ClassStats[6];

    public Player()
    {
        //loadData();
        classes = new Dictionary<string, ClassStats>();
        classes.Add("WebDev", new ClassStats("WebDev"));
        classes.Add("DataAnalytics", new ClassStats("DataAnalytics"));
        classes.Add("WebDataBases", new ClassStats("WebDataBases"));
        classes.Add("ImageProcessing", new ClassStats("ImageProcessing"));
        classes.Add("GamesDevelopment", new ClassStats("GamesDevelopment"));
        classes.Add("SoftwareDevelopment", new ClassStats("SoftwareDevelopment"));

        
    }

    public Dictionary<string , ClassStats> getClasses()
    {
        return classes;
    }

    public ClassStats[] dictToArray(Dictionary<string, ClassStats> dictionary)
    {
        string[] topicNames = { "WebDev", "GamesDevelopment", "WebDataBases", "SoftwareDevelopment", "ImageProcessing", "DataAnalytics" };

        ClassStats[] statsArray = new ClassStats[6];
        for(int count = 0; count < topicNames.Length; count++)
        {
            statsArray[count] = dictionary[topicNames[count]];
        }
        return statsArray;
    }

    public ClassStats getData(string lookUp)
    {
        return classes[lookUp];
    }

    public void updateStats(string lookupName, ClassStats updatedStats)
    {
        ClassStats updatedValue = getLevel(updatedStats);
        classes[lookupName] = updatedValue;
        SaveStats();
    }

    public void SaveStats()
    {
        ClassStats[] statsArray = dictToArray(classes);
        ClassStats currentClass;
        StreamWriter writer = new StreamWriter(Application.dataPath + "//" + fileName + ".json");

        for (int count = 0; count < classes.Count; count++)
        {
            currentClass = statsArray[count];
            writer.WriteLine(currentClass.className);       
            writer.WriteLine(currentClass.xp);
            writer.WriteLine(currentClass.level);
            writer.WriteLine(currentClass.xpTilNextLevel);           
        }
        writer.Close();
    }

    public ClassStats getLevel(ClassStats stats)
    {
        int xpTilNextLevel = stats.xpTilNextLevel;

        if(stats.xp >= xpTilNextLevel)
        {
            stats.level += 1;
            stats.xpTilNextLevel = xpTilNextLevel + (stats.getBaseLevel() + stats.getBaseLevelIncrease() + (stats.level*2));
            //getLevel(stats);
        }

        return stats;
    }


    public void loadData()
    {
        string[] topicNames = { "WebDev", "GamesDevelopment", "WebDataBases", "SoftwareDevelopment", "ImageProcessing", "DataAnalytics" };

        string className;
        int xp;
        int level;
        int xpTilNextLevel;

        string[] textRegex;
        int topicNumber = 0;

        string test;
        string currentTopic;
        StreamReader reader = new StreamReader(Application.dataPath + "//" + fileName + ".json");

        test = reader.ReadToEnd();

        textRegex = Regex.Split(test, "\n");
        Debug.Log(textRegex.Length);


        for (int count = 0; count < textRegex.Length - 1; count += 4)
        {

            currentTopic = topicNames[topicNumber];
            className = textRegex[count];
            xp = int.Parse(textRegex[count + 1]);
            level = int.Parse(textRegex[count + 2]);
            xpTilNextLevel = int.Parse(textRegex[count + 3]);

            Debug.Log(textRegex[count]);
            Debug.Log(textRegex[count + 1]);
            Debug.Log(textRegex[count + 2]);
            Debug.Log(textRegex[count + 3]);
              




            classes[currentTopic].className = currentTopic;
            classes[currentTopic].xp = xp;
            classes[currentTopic].level = level;
            classes[currentTopic].xpTilNextLevel = xpTilNextLevel;
            classes[currentTopic].setupOutputText();

            topicNumber++;

            className = "";
            xp = 0;
            level = 0;
            xpTilNextLevel = 0;
        }

        reader.Close();
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentPlayer = new Player();
        Invoke("loadData", 2.0f);     

       
    }


 
}
