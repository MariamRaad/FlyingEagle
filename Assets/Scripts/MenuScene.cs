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

    //Fade in game from play button
    private bool isEnteringLevel = false;
    private float zoomDuration = 3.0f;
    private float zoomTransition;
    public AnimationCurve enteringLevelZoomCurve;
    public RectTransform menuContainer;


    // Use this for initialization
    void Start()
    {
        //Find the only menucamera and assign it
        menuCam = FindObjectOfType<MenuCamera>();
    }

    //Update is called once per frame
    void Update()
    {
        

        //entering level zoom
       // if (isEnteringLevel)
       // {
            //StartCoroutine(WaitUntilShowingEagle());
             
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
        //}
    }

}
