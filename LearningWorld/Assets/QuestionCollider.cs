using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class QuestionCollider : MonoBehaviour {

    public GameObject answerCanvas;
    public GameObject feedbackCanvas;

    Questions questionSet;
    Answers currentQuestions;
    GameObject manager;
    GameObject toggleGroup;
    Toggle toggleOne;
    Toggle toggleTwo;
    Toggle toggleThree;
    Toggle[] toggleSet;
    Text questionText;
    GameObject player;
    ClassStats playerStats;
    string currentClassName;
    AudioSource[] audio;

	// Use this for initialization
	void Start () {
        // answerCanvas = GameObject.Find("AnswerCanvas");
        // questionText = GameObject.Find("QuestionText").GetComponent<Text>();
        questionText = answerCanvas.transform.GetChild(2).gameObject.GetComponent<Text>();
        toggleGroup = answerCanvas.transform.GetChild(1).gameObject;
        //toggleGroup = GameObject.Find("mainToggle");
        manager = GameObject.Find("GameManager");
        feedbackCanvas.SetActive(false);

        audio = this.GetComponents<AudioSource>();

        if(manager == null)
        {
            Debug.Log("manager's a lie");
        }
        else
        {
            Debug.Log("i found you braw");
        }

        toggleOne = toggleGroup.GetComponentsInChildren<Toggle>()[0];
        toggleTwo = toggleGroup.GetComponentsInChildren<Toggle>()[1];
        toggleThree = toggleGroup.GetComponentsInChildren<Toggle>()[2];



        toggleSet = new Toggle[3];

        toggleSet[0] = toggleOne;
        toggleSet[1] = toggleTwo;
        toggleSet[2] = toggleThree;

        for(int count = 0; count < toggleSet.Length; count++)
        {
            if(toggleSet[count] == null)
            {
                Debug.Log("toggleset at pos " + count + " is null");
            }
        }

        //answerCanvas.SetActive(false);
        answerCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
                          
	}

    void OnTriggerEnter(Collider collision)
    {
        if(manager == null)
        {
            Debug.Log("the manager is a lie");
        }
        manager.GetComponent<GameManager>().canCameraMove = false;
        
        player = collision.gameObject;
        playerStats = player.GetComponent<Player>().getClasses()[currentClassName];
        if(collision.tag == "Player")
        {
            answerCanvas.SetActive(true);
            /*Player player = collision.gameObject.GetComponent<Player>();
            ClassStats data = player.getData(this.gameObject.name);
            data.setXP(data.xp + 10);
            player.updateStats(this.gameObject.name, data);*/
            int randomNumber = Random.Range(0, questionSet.answers.Length);
            currentQuestions = questionSet.answers[randomNumber];
            collision.GetComponent<ControladorDePersonagem>().Anima_Personagem(0);
            toggleSet[0].isOn = true;

            // string 

            /* for(int count = 0; count < questionSet.answers.Length; count++)
             {
                 Debug.Log("question is " + questionSet.answers[count].question);
                 Debug.Log("option 1 is " + questionSet.answers[count].option1);
                 Debug.Log("option 2 is " + questionSet.answers[count].option2);
                 Debug.Log("answer is " + questionSet.answers[count].answer);
             }*/

            questionText.text = currentQuestions.question;
            setToggleText();
            answerCanvas.SetActive(true); 



           
            //Debug.Log(questionSet.answers[randomNumber].question);
        }
    }

    void setToggleText()
    {
        toggleOne.GetComponentInChildren<Text>().text = currentQuestions.answer;
        toggleTwo.GetComponentInChildren<Text>().text = currentQuestions.option1;
        toggleThree.GetComponentInChildren<Text>().text = currentQuestions.option2;

        string optionOne, optionTwo, temp = "";
        int randOne = 0 , randTwo = 0;

        for(int count = 0; count < 10; count++)
        {
            randOne = Random.Range(0, toggleSet.Length);
            randTwo = Random.Range(0, toggleSet.Length);

            while(randOne == randTwo)
            {
                randTwo = Random.Range(0, toggleSet.Length);
            }

            optionOne = toggleSet[randOne].GetComponentInChildren<Text>().text;
            optionTwo = toggleSet[randTwo].GetComponentInChildren<Text>().text;


            Debug.Log(optionOne + ":" + optionTwo);
            temp = optionTwo;
            optionTwo = optionOne;
            optionOne = temp;
            temp = "";

            toggleSet[randOne].GetComponentInChildren<Text>().text = optionOne;
            toggleSet[randTwo].GetComponentInChildren<Text>().text = optionTwo;
            
        }
        
    }

    public void updateQuestionSet(string className)
    {
        currentClassName = className;
        Questions foundQuestion = manager.GetComponent<LoadQuestions>().getQuestionByName(className);

        if (foundQuestion != null)
        {
            questionSet = foundQuestion;
        }

    }

    public void setClassName(string name)
    {
        currentClassName = name;
    }

    public void checkToggle() {
        string answer = currentQuestions.answer;
        bool wasFound = false;
        ClassStats playerStats = player.GetComponent<Player>().getClasses()[currentClassName];            

        for(int count = 0; count < toggleSet.Length; count++)
        {
            if (toggleSet[count].isOn)
            {
                if(toggleSet[count].GetComponentInChildren<Text>().text == answer)
                {
                    playerStats.setXP(playerStats.getXP() + 10);
                    player.GetComponent<Player>().updateStats(currentClassName, playerStats);
                    Debug.Log("yay you got it right");
                    feedbackCanvas.SetActive(true);
                    feedbackCanvas.transform.GetChild(0).gameObject.SetActive(true);
                    feedbackCanvas.transform.GetChild(1).gameObject.SetActive(false);
                    audio[0].mute = false;
                    audio[0].Play(0);
                    audio[1].mute = true;
                    Invoke("disableCanvas", 2.0f);
                    wasFound = true;
                }
            }
        }

        if (!wasFound)
        {
            Debug.Log("you got it wrong");
            feedbackCanvas.SetActive(true);
            feedbackCanvas.transform.GetChild(0).gameObject.SetActive(false);
            feedbackCanvas.transform.GetChild(1).gameObject.SetActive(true);
            audio[1].mute = false;
            audio[1].Play(0);
            audio[0].mute = true;
            this.GetComponent<AudioSource>().Play(0);
            Invoke("disableCanvas", 2.0f);
        }

        answerCanvas.SetActive(false);
        manager.GetComponent<GameManager>().canCameraMove = true;

    }

    void disableCanvas()
    {
        feedbackCanvas.SetActive(false);
    }

   public Answers getQuestionData()
    {
        return currentQuestions;
    }
}


