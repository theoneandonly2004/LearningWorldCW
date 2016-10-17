using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassStats
{
    public string className;
    public string outputText;
    public int xp = 0;
    public int level = 1;
    public int xpTilNextLevel = 10;

    private const int BASELEVEL = 10;
    private int baseIncreasePerLevel = 5;
    private bool hasLeveled = false;

    public ClassStats(string pClassName)
    {
        className = pClassName;

    }

    public void setupOutputText()
    {
        int nextlevel = level + 1;
        outputText = "Name:" + className + 
                     "\nLevel:" + level+
                     "\n Xp:" + xp + 
                     "\n XP needed for level " + nextlevel+":" + xpTilNextLevel;
    }

    public int getBaseLevelIncrease()
    {
        return baseIncreasePerLevel;
    }

    public int getBaseLevel()
    {
        return BASELEVEL;
    }

    public string getOutputText()
    {
        setupOutputText();
        return outputText;
    }

    public void setXP(int newXP)
    {
        xp = newXP;
    }


}

public class GameManager : MonoBehaviour {

    Dictionary<string, ClassStats> classes = new Dictionary<string, ClassStats>();

    // Use this for initialization
    void Start () {
        classes.Add("WebDev", new ClassStats("Web Development"));
        classes.Add("DataAnalytics", new ClassStats("DataAnalytics"));
        classes.Add("WebDataBases", new ClassStats("WebDataBases"));
        classes.Add("ImageProcessing", new ClassStats("ImageProcessing"));
        classes.Add("GamesDevelopment", new ClassStats("GamesDevelopment"));
        classes.Add("SoftwareDevelopment", new ClassStats("SoftwareDevelopment"));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public ClassStats getStats(string lookupName)
    {
        ClassStats stats = classes[lookupName];
        return stats;
    }

    public void updateStats(string lookupName , ClassStats updatedStats)
    {
        ClassStats updatedValue = getLevel(updatedStats);
        classes[lookupName] = updatedValue;        
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

   
}
