using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    Dictionary<string, ClassStats> classes;
    Player currentPlayer;

    public Player()
    {
        classes = new Dictionary<string, ClassStats>();
        classes.Add("WebDev", new ClassStats("Web Development"));
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

    public ClassStats getData(string lookUp)
    {
        return classes[lookUp];
    }

    public void updateStats(string lookupName, ClassStats updatedStats)
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

    void Start()
    {
        currentPlayer = new Player();
    }


 
}
