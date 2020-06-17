using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour {

    private CharacterController controller;
    private float baseSpeed = 10.0f;
    private float rotSpeedX = 5.0f;
    private float rotSpeedY = 5.0f;

	// Use this for initialization
	private void Start () {
        controller = GetComponent<CharacterController>();
   	}
	
	// Update is called once per frame
	private void Update () {
        //give the player vorwärts geschwindigkeit
        Vector3 moveVector = transform.forward * baseSpeed;

        //bestimmt den spieler input
        //transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.05f); //eigene steuerung per tasten

        //bekomme die Delta-Richtung
        Vector3 yaw = Input.GetAxis("Horizontal") * transform.right * rotSpeedX * Time.deltaTime; //Input.x
        Vector3 pitch = Input.GetAxis("Vertical") * transform.up * rotSpeedY * Time.deltaTime;    //Input.y
        Vector3 dir = yaw + pitch;
        Debug.Log("yaw: " + Input.GetAxis("Horizontal") + ", pitch: " + Input.GetAxis("Vertical"));

        //Sicherstellen, dass Player keine Loop fliegt
        float maxX = Quaternion.LookRotation(moveVector + dir).eulerAngles.x;

        //Wenn Spieler nicht zu hoch/tief fliegt, wird Richtung dem moveVector hinzugefügt
        if (maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290)
        {

        }
        else
        {
            //Richtung hinzüfugen
            moveVector += dir;

            // Spieler schaut in die Richtung in die er steuert
            transform.rotation = Quaternion.LookRotation(moveVector);
        }
        //move him
        controller.Move(moveVector * Time.deltaTime);
    }
}
