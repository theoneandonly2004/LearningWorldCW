using UnityEngine;
using System.Collections;

public class TransportMotion : MonoBehaviour {

    public GameObject target;
    NavMeshAgent agent;
    bool isPlayerCollided = false;
    bool canPlayerCollide = true;
    GameObject player;
    Vector3 startPosition;
    GameObject looktowards;
    Vector3 lookPosition;
	// Use this for initialization
	void Start () {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        startPosition = this.transform.position;
        agent.updateRotation = false;
        looktowards = GameObject.Find("testlookpoint");
        lookPosition = looktowards.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        if (isPlayerCollided)
        {
            Vector3 position = this.gameObject.transform.position;
            position.y += 3;
            player.transform.position = position;
            //agent.SetDestination(target.transform.position);

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                Debug.Log("hop out");
               // Destroy(this.gameObject);
                isPlayerCollided = false;
                canPlayerCollide = false;
                Invoke("resetCanCollide", 2.0f);
                player.transform.GetChild(0).gameObject.SetActive(true);
               //transform.LookAt(lookPosition);

            }
        }
        else
        {

            agent.SetDestination(startPosition);
            //transform.LookAt(startPosition);
        }

      

       
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            if (canPlayerCollide)
            {
                Invoke("manageRotations",5.0f);
                player = collision.gameObject;
                player.transform.GetChild(0).gameObject.SetActive(false);
                player.transform.position = this.transform.position;
                agent.SetDestination(target.transform.position);
                isPlayerCollided = true;
            }
            /*  collision.transform.position = this.transform.position;
              agent.SetDestination(target.transform.position);

              if(agent.pathStatus== NavMeshPathStatus.PathComplete)
              {
                  //Destroy(this.gameObject);
              }*/
        }
    }

    void manageRotations()
    {
        this.gameObject.transform.eulerAngles= new Vector3(0, -85, 0);
    }

    void resetCanCollide()
    {
        canPlayerCollide = true;
    }
}
