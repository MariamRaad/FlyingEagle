using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Preloader : MonoBehaviour {

    private CanvasGroup fadeGroup;
    private float loadTime;
    private float minimumLogoTime = 3.0f;

    // Use this for initialization
    void Start () {
        //grap the only canvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        //start with a white screen
        fadeGroup.alpha = 1;

        //pre load the game


        //get a timestamp of the comletion time
        //if loadtime is super give it a small buffer time so we can appreciate the logo
        if(Time.time<minimumLogoTime){
            loadTime = minimumLogoTime;
        }else{
            loadTime = Time.time;
        }

    }
	
	// Update is called once per frame
	void Update () {
        //fade in
        if(Time.time < minimumLogoTime){
            fadeGroup.alpha = 1 - Time.time;
        }
        //fade out;
        if(Time.time > minimumLogoTime && loadTime != 0){
            fadeGroup.alpha = Time.time - minimumLogoTime;
                if(fadeGroup.alpha >= 1){
                SceneManager.LoadScene("Menu");

                }
        }
	}
}
