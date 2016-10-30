using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class toggleButtonControl : MonoBehaviour {

    Toggle toggleOne;
    Toggle toggleTwo;
    Toggle toggleThree;

    static Answers question;

    string answer = question.answer;
    GameObject answerCanvas;


   /* void Start()
    {
        answerCanvas = GameObject.Find("mainToggle"); 
        
        if(answerCanvas != null)
        {
            toggleOne = answerCanvas.GetComponentsInChildren<Toggle>()[0];
            toggleTwo = answerCanvas.GetComponentsInChildren<Toggle>()[1];
            toggleThree = answerCanvas.GetComponentsInChildren<Toggle>()[2];
        }       
    }*/

    public void CheckAnswer()
    {
        QuestionCollider collider = GameObject.Find("QuestionProvider").GetComponent<QuestionCollider>();
        collider.checkToggle();
       // Debug.Log("hello world");
    }
    
}
