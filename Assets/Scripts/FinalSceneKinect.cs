using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalSceneKinect : MonoBehaviour
{
    public GameObject BodySrcManager;
    public JointType LeftHand;
    public JointType RightHand;

    private BodySrcManager bodyManager;
    private Body[] bodies;
    private Vector3 bodyI;  //initale Personen-Position
    private bool updatePos = false;

    //Fade in game from play button
    public Timer myTimer;
    public Text playingTime;
    private CanvasGroup fadeGroup;
    private GameObject menuButton;
    private float fadeInSpeed = 0.33f;
    private float fadeInDuration = 2;
    private bool gameStarted;
    private bool goingBack = false;
    private bool isButtonTriggered = false;

    // Use this for initialization
    void Start()
    {
        //grap the only canvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();
        menuButton = GameObject.FindWithTag("menuButton");

        //start with a white screen
        fadeGroup.alpha = 1;


        //Timer anzeigen
        myTimer = GetComponent<Timer>();
        playingTime = GetComponent<Text>() as Text;
        playingTime.text = "Zeit: " + myTimer.getMinutes().ToString("00") + ":" + myTimer.getSeconds().ToString("00");


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

                    /*
                    //auslesen der initialen Personen-Koord. aus Kinect (einmalig!)
                    Vector3 bodyI_LeftHand = new Vector3(body.Joints[LeftHand].Position.X, body.Joints[LeftHand].Position.Y, body.Joints[LeftHand].Position.Z);
                    Vector3 bodyI_RightHand = new Vector3(body.Joints[RightHand].Position.X, body.Joints[RightHand].Position.Y, body.Joints[RightHand].Position.Z);
                    */
                }

                //auslesen der aktuellen Personen-Koord. aus Kinect
                Vector3 bodyT_LeftHand = new Vector3(body.Joints[LeftHand].Position.X, body.Joints[LeftHand].Position.Y, body.Joints[LeftHand].Position.Z);
                Vector3 bodyT_RightHand = new Vector3(body.Joints[RightHand].Position.X, body.Joints[RightHand].Position.Y, body.Joints[RightHand].Position.Z);

                //Gestenerkennung: Berühren des Buttons mit rechter oder linker Hand
                if (isButtonTriggered)
                {
                    //Übergang zu Menu-Scene
                    goingBack = true;
                }

                //entering level zoom
                if (goingBack)
                {
                    //Enter the level
                    SceneManager.LoadScene("Menu");
                }
            }

        }
    }

    //TODO Trigger-Funktion
    /*void OnTriggerEnter(Collider other)
    {
        if (enter)
        {
            Debug.Log("entered");
        }
    }
    */
}