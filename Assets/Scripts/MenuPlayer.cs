using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	private void Update () {
        transform.position += Vector3.forward * 5 * Time.deltaTime;	
	}
}
