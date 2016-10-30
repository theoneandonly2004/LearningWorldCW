
using UnityEngine;
using System.Collections;

public class ControleOrbital : MonoBehaviour {

    private float vertical;
    private float velcoidadeDeGiro = 4.0f;

    private GameManager manager;
    private bool canCameraMove;
    void Start ()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        vertical = transform.eulerAngles.x;
    }
	
	void Update ()
    {
        canCameraMove = manager.getCanCameraMove();

        if (canCameraMove)
        {
            var mouseVertical = Input.GetAxis("Mouse Y");
            vertical = (vertical - velcoidadeDeGiro * mouseVertical) % 360f;
            vertical = Mathf.Clamp(vertical, -30, 60);
            transform.localRotation = Quaternion.AngleAxis(vertical, Vector3.right);
        }
    }
}
