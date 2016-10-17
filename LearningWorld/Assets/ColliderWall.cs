using UnityEngine;
using System.Collections;

public class ColliderWall : MonoBehaviour {
    public string[] requirementClass;
    public int[] requiredLevels;
    bool canProceed = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        string collidedTag = collision.gameObject.tag;

        if(collidedTag == "Player")
        {
            Player data = collision.gameObject.GetComponent<Player>();
            
            for(int count =0; count < requirementClass.Length; count++)
            {
                string classLookup = requirementClass[count];
                int requiredLevel = requiredLevels[count];

                if(data.getData(classLookup).level < requiredLevel)
                {
                    canProceed = false;
                    Debug.Log("you need to learn more " + classLookup + " to proceed");
                    Debug.Log("you need at least level " + requiredLevel + " in " + classLookup);
                }
            }

            if(canProceed == true)
            {
                Debug.Log("congrats you can proceed");
            }
            
            
        }
    }

    void OnTriggerExit(Collider collision)
    {
        canProceed = true;
    }
}
