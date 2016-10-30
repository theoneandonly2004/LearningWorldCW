using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ImageButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;
    Text displayText;
    GameObject canvas;


     string[] requirementClass;
     int[] requiredLevels;
    bool canProceed = true;


    GameManager manager;

    // Use this for initialization
    void Start () {
	    manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        displayText = GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void MouseDown()
    {
        canProceed = true;
        GameObject player = GameObject.Find("Player");
        GameObject requiredDoor;
        Player data = player.GetComponent<Player>();
        Vector3 swapPosition;

        requiredDoor = GameObject.FindGameObjectWithTag(this.name);
        ColliderWall doorData = requiredDoor.GetComponent<ColliderWall>();

        canvas = doorData.getCanvas();
        requiredLevels = doorData.getRequirementLevels();
        requirementClass = doorData.getRequiredClasses();

        Debug.Log(requiredLevels[0]);
        string currentMessage = "";

        for (int count = 0; count < requirementClass.Length; count++)
        {
            string classLookup = requirementClass[count];
            int requiredLevel = requiredLevels[count];

            if (data.getData(classLookup).level < requiredLevel)
            {
                canProceed = false;
                Debug.Log("you need to learn more " + classLookup + " to proceed");
                Debug.Log("you need at least level " + requiredLevel + " in " + classLookup);
                currentMessage = currentMessage + "\n you need at least level " + requiredLevel + " in " + classLookup;
            }
        }

        if (canProceed == true)
        {
            Debug.Log("hooray you can move");
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            // swapPosition = this.transform.GetChild(0).gameObject.transform.position;
            Vector3 returnPosition = requiredDoor.transform.GetChild(0).transform.position;
            Vector3 returnRotation = requiredDoor.transform.GetChild(0).transform.eulerAngles;

            Debug.Log(returnRotation);
            //GameObject.Find("ExitPoint").GetComponent<ExitIsland>().setReturnPosition(returnPosition);
            GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
            Debug.Log(exitPoints.Length);
            for (int count = 0; count < exitPoints.Length; count++)
            {
                exitPoints[count].GetComponent<ExitIsland>().setReturnPosition(returnPosition);
                exitPoints[count].GetComponent<ExitIsland>().setReturnRotation(returnRotation);
            }

            swapPosition = GameObject.Find("IslandEntryPoint").transform.position;         
            player.transform.localPosition = swapPosition;

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

    void disableCanvas()
    {
        canvas.SetActive(false);
    }
       
    public void OnPointerEnter(PointerEventData eventData)
    {        
        isOver = true;
        Debug.Log(this.gameObject.name + " was mouse over");
        displayText.enabled = true;
        //ClassStats stats = manager.getStats(this.gameObject.name);
        ClassStats stats = GameObject.Find("Player").GetComponent<Player>().getData(this.gameObject.name);
        //stats.xp += 10;
       // manager.updateStats(stats.className, stats);

        displayText.text = stats.getOutputText();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isOver = false;
        displayText.enabled = false;
       
    }
}
