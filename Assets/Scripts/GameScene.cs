using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
    public Transform arrow;
    private Transform playerTransform;
    public Objective objective;

    // Use this for initialization
    void Start () {
        //lets find the player transform
        playerTransform = FindObjectOfType<PlayerMotor>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (objective != null)
        {
            //if we have an object

            //rotate the arrow
            Vector3 dir = playerTransform.InverseTransformPoint(objective.GetCurrentRing().position);
            float a = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            a += 180;
            arrow.transform.localEulerAngles = new Vector3(0, 180, a);
        }
    }
}
