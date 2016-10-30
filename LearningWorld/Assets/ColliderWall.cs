using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ColliderWall : MonoBehaviour {
    public string[] requirementClass;
    public int[] requiredLevels;
    bool canProceed = true;

    public GameObject canvas;

    public GameObject getCanvas()
    {
        return canvas;
    }

    public string[] getRequiredClasses()
    {
        return requirementClass;
    }

    public int[] getRequirementLevels()
    {
        return requiredLevels;
    }

    Collider playerCollided;
    Vector3 swapPosition;
	// Use this for initialization
	void Start () {
        canvas.SetActive(false); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {

        string collidedTag = collision.gameObject.tag;
        
        if(collidedTag == "Player")
        {
            playerCollided = collision;
            Player data = collision.gameObject.GetComponent<Player>();
            string currentMessage = "";
            
            for(int count =0; count < requirementClass.Length; count++)
            {
                string classLookup = requirementClass[count];
                int requiredLevel = requiredLevels[count];

                if(data.getData(classLookup).level < requiredLevel)
                {
                    canProceed = false;
                    Debug.Log("you need to learn more " + classLookup + " to proceed");
                    Debug.Log("you need at least level " + requiredLevel + " in " + classLookup);
                    currentMessage = currentMessage + "\n you need at least level " + requiredLevel + " in " + classLookup;
                }
            }

            if(canProceed == true)
            {
                //this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                // swapPosition = this.transform.GetChild(0).gameObject.transform.position;
                Vector3 returnPosition = this.transform.GetChild(0).transform.position;
                Vector3 returnRotation = this.transform.GetChild(0).transform.eulerAngles;

                Debug.Log(returnRotation);
                //GameObject.Find("ExitPoint").GetComponent<ExitIsland>().setReturnPosition(returnPosition);
                GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
                Debug.Log(exitPoints.Length);
                for(int count = 0; count < exitPoints.Length; count++)
                {
                    exitPoints[count].GetComponent<ExitIsland>().setReturnPosition(returnPosition);
                    exitPoints[count].GetComponent<ExitIsland>().setReturnRotation(returnRotation);
                }

                swapPosition = GameObject.Find("IslandEntryPoint").transform.position;
                Debug.Log("swap pos: " + swapPosition);
                Debug.Log("player pos: " + collision.gameObject.transform.position);
                collision.gameObject.transform.localPosition = swapPosition;

                //set questions in here
                GameObject questionProvider = GameObject.Find("QuestionProvider");
                questionProvider.GetComponent<QuestionCollider>().updateQuestionSet(this.gameObject.name);   
               
            }

            else
            {
                canvas.GetComponentInChildren<Text>().text = currentMessage;
                canvas.SetActive(true);
                Invoke("disableCanvas", 2.0f);
            }
            
            
        }

    }

    

    void disableCanvas()
    {
        canvas.SetActive(false);
    }

    void OnTriggerExit(Collider collision)
    {
        canProceed = true;
    }
}
