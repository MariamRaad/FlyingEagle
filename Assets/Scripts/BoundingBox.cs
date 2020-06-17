using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour {

    private CharacterController player;

    // Use this for initialization
    void Start ()
    {
        player = FindObjectOfType<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Wenn Adler linke BoundingBox berührt
    private void OnTriggerEnter(Collider other)
    {
        //dann soll der Adler in y-Achse-Richtung verändert werden
        //Parameter sind: xAngle, yAngle, zAngle, relativeTo	(relativeTo=Rotation is local to object or World)
        player.transform.Rotate(180, Time.deltaTime * 30, 0, Space.Self);


    }
}
