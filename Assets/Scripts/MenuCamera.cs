using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour {
    private Vector3 startPosition;
    private Quaternion startRotation;

    private Vector3 desiredPosition;
    private Quaternion desiredRotation;
    
	// Use this for initialization
	void Start () {
        startPosition = desiredPosition = transform.localPosition;
        startRotation = desiredRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.localPosition = Vector3.Lerp(transform.localPosition, desiredPosition, 0.1f);
        transform.localRotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.1f);


	}
}
