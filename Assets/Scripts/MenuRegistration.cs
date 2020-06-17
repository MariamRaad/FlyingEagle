using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuRegistration : MonoBehaviour
{

    public GameObject BodySrcManager;

    private BodySrcManager bodyManager;
    private Body[] bodies;
    private Vector3 bodyI;  //initale Personen-Position
    private bool updatePos = false;

    //Joints am Körper für die Winkelberechnung
    static private Vector3 bodyT_Neck;
    public JointType Neck;
    public JointType LeftHand;
    public JointType RightHand;
    public JointType BodySpine;

    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;
    private float fadeInDuration = 2;
    private bool gameStarted;
    private MenuCamera menuCam;
    private GameObject player;

    //Fade in game from T-Pose-Registration
    public Image fadeOutTPose;
    public Animator anim;
    private GameObject tPose;
    private GameObject tPoseGlow;
    private Vector3 eagleStart = new Vector3(0, 40, 30);

    //Übergang zu Gameszene
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

        //start with a white screen
        player = GameObject.FindWithTag("PlayerEagle");
        tPose = GameObject.FindWithTag("tpose");
        tPoseGlow = GameObject.FindWithTag("tposeGlow");
        tPoseGlow.SetActive(false);
        anim = player.GetComponent<Animator>();

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
    void Update()
    {

        //fade in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
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
                if (!updatePos)
                {
                    updatePos = true; //bei Game Over auf true
                }

                //Registrierung: T-Pose erkennen
                //auslesen der aktuellen Personen-Koord. aus Kinect
                bodyT_Neck = new Vector3(body.Joints[Neck].Position.X, body.Joints[Neck].Position.Y, body.Joints[Neck].Position.Z);
                Vector3 bodyT_LeftHand = new Vector3(body.Joints[LeftHand].Position.X, body.Joints[LeftHand].Position.Y, body.Joints[LeftHand].Position.Z);
                Vector3 bodyT_RightHand = new Vector3(body.Joints[RightHand].Position.X, body.Joints[RightHand].Position.Y, body.Joints[RightHand].Position.Z);
                Vector3 bodyT_BodySpine = new Vector3(body.Joints[BodySpine].Position.X, body.Joints[BodySpine].Position.Y, body.Joints[BodySpine].Position.Z);

                //Definiere Richtungsvektor von linker/rechter Hand zu Neck
                Vector3 vector_HandLToNeck = (bodyT_LeftHand - bodyT_Neck);
                Vector3 vector_HandRToNeck = (bodyT_RightHand - bodyT_Neck);

                //Definiere Richtungsvektor von BodySpine zu Neck
                Vector3 vector_BodySpineToNeck = (bodyT_BodySpine + bodyT_Neck);

                //Berechne linken/rechten Winkel beider Richtungsvektoren
                float angleLeft = Vector3.Angle(vector_HandLToNeck, vector_BodySpineToNeck);
                float angleRight = Vector3.Angle(vector_HandRToNeck, vector_BodySpineToNeck);
                //Debug.Log("angleLeft : " + angleLeft + "angleRight : " + angleRight);

                //Wechsel in die Gameszene nur, wenn linker und rechter Arm ca. 90 Grand Winkel annehmen
                if ((angleLeft > 70.0f && angleLeft < 100.0f) && (angleRight > 70.0f && angleRight < 100.0f))
                {
                    //Übergang zu Game-Scene
                    isEnteringLevel = true;

                    Debug.Log("Spieler hat T-Pose eingenommen!");
                    tPoseGlow.SetActive(true);
                    tPose.SetActive(false);
                }

                //entering level zoom
                if (isEnteringLevel)
                {
                    Debug.Log("Spieler betritt das Level!");
                    StartCoroutine(WaitForAnimations());


                    //fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

                    //player.GetComponent<MeshRenderer>().material.color = Color.Lerp(player.GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
                    // player.transform.localScale += new Vector3(50f, 50f, 50f);

                    //Enter the level
                    //SceneManager.LoadScene("Game");

                    /*
                    //folgender Code führt dazu, dass bei T-Pose-Erkennen das ins T-Bild reingezoomt wird
                    
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
                    }*/

                }

            }
        }


    }

    public Vector3 getNeckRegistration()
    {
        Debug.Log("registrationData: " + bodyT_Neck);
        return bodyT_Neck;
    }

    IEnumerator WaitForAnimations()
    {
        Debug.Log("wait for Animations");
        yield return new WaitForSeconds(2);
        fadeOut();
        Debug.Log("wait is over");
        player.transform.position = eagleStart;
        anim.SetTrigger("Active");
        yield return new WaitForSeconds(2);
        zoomIn();
    }

    void fadeOut()
    {
        fadeOutTPose.CrossFadeAlpha(0, 2, false);
    }

    void zoomIn()
    {
        Debug.Log("ist drin vor zoomtransition");
        ////add to the zoomtransition float
        zoomTransition += (1 / zoomDuration) * Time.deltaTime;
        Debug.Log("ist drin vor local scale");
        ////change the scale following the animationcurve
        menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 15, enteringLevelZoomCurve.Evaluate(zoomTransition));
        Debug.Log("ist drin vor alpha fade");
        ////fade to whitescreen, this will overwrite the first line of the update
        fadeGroup.alpha = zoomTransition;
        Debug.Log("ist drin vor if");
        ////if the animation is done
        if (zoomTransition >= 1)
        {
            Debug.Log("ist drin in if");
            //Enter the level
            SceneManager.LoadScene("Game");
        }
    }
}