using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour {

    private Objective objectiveScript;
    private bool ringActive = false;
    private void Start()
    {
        objectiveScript = FindObjectOfType<Objective>();

    }

    public void activateRing(){
        ringActive = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if the ring is active tell the objective script that it has been passed through
        if(ringActive){
            objectiveScript.nextRing();
            Destroy(gameObject,2.0f);
        }
    }
    
}
