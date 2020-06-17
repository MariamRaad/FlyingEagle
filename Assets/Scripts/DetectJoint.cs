using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class DetectJoint : MonoBehaviour
{
     public GameObject BodySrcManager;
     public JointType TrackedJoint;
     public float multiplier = 1.0f;
     public float speed;
     public GameObject player; // Adler-Objekt
    private Vector3 lastFramePitch;

     private BodySrcManager bodyManager;
     private Body[] bodies;
     private Vector3 bodyI;  //initale Personen-Position
     private Vector3 playerI; //initale Adler-Position
     private bool updatePos = false;
     private float posZ = 0f;

     private CharacterController controller;
    public float baseSpeed = 5.0f;
    public float rotSpeedX = 5.0f;
    public float rotSpeedY = 5.0f;

    private Vector3 bodyT;

    // Use this for initialization
    void Start()
    {

        controller = GetComponent<CharacterController>();

        if (BodySrcManager == null)
        {
            Debug.Log("bodysrc null");
        }
        else
        {
            bodyManager = BodySrcManager.GetComponent<BodySrcManager>();
        }
        //setze Personen-Position = Adler-Position
        /*
        //TODO
        //müsste das nicht heißen: bodyI = player.transform.position;
        //die initiale Adler-Position wird auf die Position des Spielers gemapped
        //Spieler bekommt Koordinaten vom Adler oder andersrum?
        */
        //bodyI = player.transform.position;
        playerI = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            updatePos = false;
            

        }
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
                if (!updatePos)
                {
                    lastFramePitch = new Vector3(0, 0, 0);
                    updatePos = true; //bei Game Over auf true

                    //auslesen der initialen Personen-Koord. aus Kinect (einmalig!)
                    bodyI = new Vector3(body.Joints[TrackedJoint].Position.X, body.Joints[TrackedJoint].Position.Y, body.Joints[TrackedJoint].Position.Z);
                    Debug.Log("save position");
                }

                //auslesen der aktuellen Personen-Koord. aus Kinect
                bodyT = new Vector3(body.Joints[TrackedJoint].Position.X, body.Joints[TrackedJoint].Position.Y, body.Joints[TrackedJoint].Position.Z);

                //automatische Fortbewegung in z-Richtung
                //speed wird in Unity-Eingabefeld gesetzt
                posZ += speed;

                //TODO nur auf x-Achse festlegen, sodass in die Hocke gehen nicht möglich ist?
                //Berechnung des  Offsets damit Spieler und Adler aufeinander gemapped werden
                //player.transform.position = playerI + (bodyT - bodyI) * multiplier;
                //Debug.Log("update body is tracked" + bodyI + bodyT + player.transform.position);

                /*
                //Damit vor und zurücklehnen unterschieden wird mit in die Hocke gehen, muss weiter oben die y-Richtung deaktiviert werden --> Links/Rechts-Bewegung darf auch nur auf x-Achse funktionieren
                //Vorlehnen um runter zu fliegen
                float z_value = bodyI.z - bodyT.z;
                Debug.Log("bodyI.z: " + bodyI.z);
                Debug.Log("bodyT.z: " + bodyT.z);
                Debug.Log("z_value: " + z_value);

                if (z_value > bodyT.z)
                {
                Debug.Log("vorlehnen");
                }

                //Zurücklehnen um hoch zu fliegen
                else if (z_value < bodyT.z)
                {
                Debug.Log("zurücklehnen");
                }
                */
            }

                //Kamera-Steuerung/Drehung
                //give the player forward movement
                Vector3 moveVector = transform.forward * baseSpeed;
                // Vector3 moveVector = new Vector3(0f, 0f, posZ);

                //bestimmt den spieler input
                //transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.05f); //eigene steuerung per tasten

                //bekomme die Delta-Richtung
                Vector3 delta = 1f*(bodyT - bodyI);
                
                Vector3 yaw = delta.x * transform.right * rotSpeedX * Time.deltaTime; //Input.x
                Vector3 pitch = delta.y * transform.up * rotSpeedY;    //Input.y
                Vector3 direction = yaw + pitch;

                Vector3 deltaPitch = pitch - lastFramePitch;

                lastFramePitch = pitch;

                Debug.Log("yaw: " + delta.x + ", pitch: " + delta.y);
            //adler dreht sich immer um 180 grad die lookrotation --> wenn delta <0 +180
                //Sicherstellen, dass Player keine Loop fliegt
                float maxX = Quaternion.LookRotation(moveVector + direction).eulerAngles.x;

                //Wenn Spieler nicht zu hoch/tief fliegt, wird Richtung dem moveVector hinzugefügt
                if (maxX < 90 && maxX > 70 || maxX > 270 && maxX < 290)
                {

                }
                else
                {
                    //Richtung hinzüfugen
                    moveVector += yaw + deltaPitch;


                // Spieler schaut in die Richtung in die er steuert
                transform.rotation = Quaternion.LookRotation(moveVector);
                }
                //move him
                controller.Move(moveVector * Time.deltaTime);
            }
        }

    }