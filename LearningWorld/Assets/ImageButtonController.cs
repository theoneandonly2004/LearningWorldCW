using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class ImageButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;
    Text displayText;

    GameManager manager;

    // Use this for initialization
    void Start () {
	    manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        displayText = GetComponentInChildren<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {

        Debug.Log("i am over");
        isOver = true;

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
