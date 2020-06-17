using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuScene : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;
    private float fadeInDuration = 2;
    private bool gameStarted;
    private MenuCamera menuCam;
    private GameObject player;

    //Fade in game from play button
    private bool isEnteringLevel = false;
    private float zoomDuration = 3.0f;
    private float zoomTransition;
    public AnimationCurve enteringLevelZoomCurve;
    public RectTransform menuContainer;

    private Color alphaColor;
    private float timeToFade = 1.0f;

   

    // Use this for initialization
    void Start()
    {


        //Find the only menucamera and assign it
        menuCam = FindObjectOfType<MenuCamera>();

        //grap the only canvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        //start with a white screen
        fadeGroup.alpha = 1;
        player = GameObject.FindWithTag("PlayerEagle");

        alphaColor = player.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
    }

    //Update is called once per frame
    void Update()
    {

        //fade in

        //entering level zoom
        if (isEnteringLevel)
        {
            fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
             
            ////add to the zoomtransition float
            //zoomTransition += (1 / zoomDuration) * Time.deltaTime;
            ////change the scale following the animationcurve
            //menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 15, enteringLevelZoomCurve.Evaluate(zoomTransition));
            ////fade to whitescreen, this will overwrite the first line of the update
            //fadeGroup.alpha = zoomTransition;

            ////if the animation is done
            //if (zoomTransition >= 1)
            //{

            //    //Enter the level
            //    SceneManager.LoadScene("Game");
            //}
        }
    }

    public void OnPlayClick()
    {
        isEnteringLevel = true;
        player.GetComponent<MeshRenderer>().material.color = Color.Lerp(player.GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
        player.transform.localScale += new Vector3(50f, 50f, 50f);
        Debug.Log("play clicked");
    }


}
