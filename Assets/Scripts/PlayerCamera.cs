using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {
    public Transform lookAt;

    private Vector3 desiredPosition;
    public float offset = 1.5f;
    public float distance = 3.5f;
    public float lerp = 0.05f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
        desiredPosition = lookAt.position + (-transform.forward * distance) + (transform.up * offset);
        transform.position = Vector3.Lerp(transform.position, desiredPosition, lerp);
        transform.LookAt(lookAt.position + (Vector3.up * offset));
    }
}
