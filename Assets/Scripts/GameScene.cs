using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour {
    public Transform arrow;
    private Transform playerTransform;
    public Objective objective;
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

    // Use this for initialization
    void Start () {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        //start with a white Screen
        fadeGroup.alpha = 1;
        //lets find the player transform
        playerTransform = FindObjectOfType<PlayerMotor>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
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
