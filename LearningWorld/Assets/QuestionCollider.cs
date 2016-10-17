using UnityEngine;
using System.Collections;

public class QuestionCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            ClassStats data = player.getData(this.gameObject.name);
            data.setXP(data.xp + 10);
            player.updateStats(this.gameObject.name, data);
        }
    }
}
