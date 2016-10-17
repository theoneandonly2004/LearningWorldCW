using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour {

    Dictionary<string, ClassStats> classes;
    public PlayerData()
    {
        
        classes = new Dictionary<string, ClassStats>();
        classes.Add("WebDev", new ClassStats("Web Development"));
        classes.Add("DataAnalytics", new ClassStats("DataAnalytics"));
        classes.Add("WebDataBases", new ClassStats("WebDataBases"));
        classes.Add("ImageProcessing", new ClassStats("ImageProcessing"));
        classes.Add("GamesDevelopment", new ClassStats("GamesDevelopment"));
        classes.Add("SoftwareDevelopment", new ClassStats("SoftwareDevelopment"));
    }
}
