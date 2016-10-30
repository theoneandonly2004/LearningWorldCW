using UnityEngine;
using System.Collections;

public class ExitIsland : MonoBehaviour {
    Vector3 returnPosition;
    Vector3 returnRotation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if(returnPosition != null)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.transform.position = returnPosition;
                collision.gameObject.transform.eulerAngles = returnRotation;

                Debug.Log("rotation is " + collision.gameObject.transform.eulerAngles);
            }
        }
        else
        {
            Debug.Log("i don't have a return position");
        }
    }

    public void setReturnPosition(Vector3 position)
    {
        returnPosition = position;
    }

    public void setReturnRotation(Vector3 rotate)
    {
        returnRotation = rotate;
    }

}
