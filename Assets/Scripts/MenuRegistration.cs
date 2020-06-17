using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.SceneManagement;

public class MenuRegistration : MonoBehaviour {

    public GameObject BodySrcManager;
    public JointType bodySpine;
    public JointType LeftHand;
    public JointType RightHand;

    private BodySrcManager bodyManager;
    private Body[] bodies;
    private Vector3 bodyI;  //initale Personen-Position
    private bool updatePos = false;

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;
    private float fadeInDuration = 2;
    private bool gameStarted;
    private MenuCamera menuCam;
    private GameObject player;
    //Fade in game from T-Pose-Registration
    private bool isEnteringLevel = false;
    private float zoomDuration = 3.0f;
    private float zoomTransition;
    public AnimationCurve enteringLevelZoomCurve;
    public RectTransform menuContainer;
    private Color alphaColor;
    private float timeToFade = 1.0f;



	// Use this for initialization
	void Start () {

	    //Find the only menucamera and assign it
        menuCam = FindObjectOfType<MenuCamera>();

        //grap the only canvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        //start with a white screen
        fadeGroup.alpha = 1;
        player = GameObject.FindWithTag("PlayerEagle");

        alphaColor = player.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;

		if (BodySrcManager == null)
        {
            Debug.Log("bodysrc null");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySrcManager>();
        }
	}
	
	// Update is called once per frame
	void Update () {

	    //fade in
        //TODO führt dazu, dass T-Pose-Bild verschwindet?
        //fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

		if (bodyManager == null)
        {
            Debug.Log("update bodysrc null");
            return;
        }

        bodies = bodyManager.GetData();

        if (bodies == null)
        {
            Debug.Log("update bodies null");
            return;
        }

        //multiplier auf jede Person anpassen, im Moment nur für Person 1  und dann Versatz
        foreach (var body in bodies)
        {
            if (body == null)
            {
                Debug.Log("update body null");
                continue;
            }
            if (body.IsTracked)
            {
                Debug.Log("body is tracked!");
                if(!updatePos)
                {
                    updatePos = true; //bei Game Over auf true

                    /*
                    //auslesen der initialen Personen-Koord. aus Kinect (einmalig!)
                    Vector3 bodyI_bodySpine = new Vector3(body.Joints[bodySpine].Position.X, body.Joints[bodySpine].Position.Y, body.Joints[bodySpine].Position.Z);
                    Vector3 bodyI_LeftHand = new Vector3(body.Joints[LeftHand].Position.X, body.Joints[LeftHand].Position.Y, body.Joints[LeftHand].Position.Z);
                    Vector3 bodyI_RightHand = new Vector3(body.Joints[RightHand].Position.X, body.Joints[RightHand].Position.Y, body.Joints[RightHand].Position.Z);
                    */
                }

                //auslesen der aktuellen Personen-Koord. aus Kinect
                Vector3 bodyT_bodySpine = new Vector3(body.Joints[bodySpine].Position.X, body.Joints[bodySpine].Position.Y, body.Joints[bodySpine].Position.Z);
                Vector3 bodyT_LeftHand = new Vector3(body.Joints[LeftHand].Position.X, body.Joints[LeftHand].Position.Y, body.Joints[LeftHand].Position.Z);
                Vector3 bodyT_RightHand = new Vector3(body.Joints[RightHand].Position.X, body.Joints[RightHand].Position.Y, body.Joints[RightHand].Position.Z);

                //Registrierung: T-Pose erkennen
                //entweder wenn y-Positionen der Hand-Joints auf Höhe vom Neck-Joint sind
                //oder wenn die Winkel zwischen dem Spine-Base-Joint und den Hand-Joints bestimmte Werte haben
                float rightHand_Angle = Vector3.Angle(bodyT_bodySpine, bodyT_RightHand);
                float leftHand_Angle = Vector3.Angle(bodyT_bodySpine, bodyT_LeftHand);

                //T-Pose des Spielers
                //TODO wird nur ein einziges mal erkannt - wieso? --> Ausgabe gibt es nur einmal, aber Zeit zählt weiter, d.h. der Zustand wird erkannt
                if ((rightHand_Angle > 25 && rightHand_Angle < 40) && (leftHand_Angle > 25 && leftHand_Angle < 40))
                {
                    //Übergang zu Game-Scene
                    isEnteringLevel = true;
                    Debug.Log("Spieler hat T-Pose eingenommen!");
                }

	            //entering level zoom
                if (isEnteringLevel)
                {

                    Debug.Log("Spieler betritt das Level!");
                    
                    fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

                    player.GetComponent<MeshRenderer>().material.color = Color.Lerp(player.GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
                    player.transform.localScale += new Vector3(50f, 50f, 50f);

                    //Enter the level
                    SceneManager.LoadScene("Game");


                    //folgender Code führt dazu, dass bei T-Pose-Erkennen das ins T-Bild reingezoomt wird
                    /*
                    //add to the zoomtransition float
                    zoomTransition += (1 / zoomDuration) * Time.deltaTime;
                    //change the scale following the animationcurve
                    menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 15, enteringLevelZoomCurve.Evaluate(zoomTransition));
                    //fade to whitescreen, this will overwrite the first line of the update
                    fadeGroup.alpha = zoomTransition;
                    
                    //if the animation is done
                    if (zoomTransition >= 1)
                    {
                        //Enter the level
                        SceneManager.LoadScene("Game");
                    }
                     
                     */

                }

	        }
        }


    }
}