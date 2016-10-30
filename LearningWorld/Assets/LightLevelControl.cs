using UnityEngine;
using System.Collections;

public class LightLevelControl : MonoBehaviour {
    Light lightObject;
    float currentIntensity;
    float lightStep = 0.1f;
    public int maxLevel = 1;
    bool canChange , isIncreasing = true;

	// Use this for initialization
	void Start () {
        lightObject = this.GetComponent<Light>();
        InvokeRepeating("change", 10.0f, 10.0f);
	}
	
	// Update is called once per frame
	void Update () {       
	

    }

    void change()
    {
        currentIntensity = lightObject.intensity;
        Debug.Log(currentIntensity + ":" + maxLevel);
        if (isIncreasing) {
            if (currentIntensity < maxLevel) {
                lightObject.intensity += lightStep;
            }
            else
            {
                isIncreasing = false;
                CancelInvoke("change");
                InvokeRepeating("change", 120f, 15.0f);
            }
        }
        else
        {
            if (currentIntensity >= 0)
            {
                lightObject.intensity -= lightStep;
            }
            else
            {
                isIncreasing = true;
                CancelInvoke("change");
                InvokeRepeating("change", 120f, 15.0f);
            }
        }
    }
}
