using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Objective : MonoBehaviour {
    private Timer timer;
    private RingCounter ringCounter;
    public Material activeRing; //nächster ring zum durchfliegen
    public Material inactiveRing; //noch nicht durchflogen
    public Material finalRing;
    public int ringPassed = 0;
    public int ringeGesamt = 0;
    private List<Transform> rings = new List<Transform>();


    private void Start()
    {
        //set the objective field in the menu scene
        FindObjectOfType<GameScene>().objective = this;

        timer = FindObjectOfType<Timer>();
        ringCounter = FindObjectOfType<RingCounter>();
        //at the start of the level assign inactive to all rings
        foreach (Transform t in transform){
            rings.Add(t);
            t.GetComponent<MeshRenderer>().material = inactiveRing;
            //t.GetComponent<ParticleSystem>().Stop();//deaktivate particle system
            if (rings.Count == 0){
                Debug.Log("There is no objectives");
                return;
            }

            //activate the first ring
            rings[ringPassed].GetComponent<MeshRenderer>().material = activeRing;
            rings[ringPassed].GetComponent<Ring>().activateRing();

        }
        ringeGesamt = rings.Count;
    }

    public void nextRing(){
        rings[ringPassed].GetComponent<MeshRenderer>().material = inactiveRing;
        rings[ringPassed].GetComponent<Animator>().SetTrigger("collectionTrigger");
        //erhöhe counter
        ringPassed++;
        //übergebe anzahl ringe an ringCounter skript
        ringCounter.countRings(ringeGesamt);

        //if it is the finalRing lets call the victory
        if (ringPassed == rings.Count){
            finishedGame();
            timer.stopTimer();
            SceneManager.LoadScene("FinalScene");
            return;
        }
        
        //if this is the previous last give the next ring the final ring material
        if(ringPassed == rings.Count-1){
            rings[ringPassed].GetComponent<MeshRenderer>().material = finalRing;
            //rings[ringPassed].GetComponent<ParticleSystem>().Play(); //make it shine or sth like that
        }
        else{
            rings[ringPassed].GetComponent<MeshRenderer>().material = activeRing;
        }

        //in both cases we need to activate the ring
        rings[ringPassed].GetComponent<Ring>().activateRing();
    }

    //SOBALD HIER ETW REINGESCHRIEBEN WIRD BRICHT DAS SPIEL BEREITS BEI DEM RING VOR FINAL RING AB?!
    private void finishedGame(){

        //Spiel ist beendent da finalRing durchquert wurde --> LOAD FINALSCENE
        Debug.Log("finished game");
        //SceneManager.LoadScene("FinalScene");

    }

    public Transform GetCurrentRing(){
        return rings[ringPassed];
    }

    public int GetRingPassed(){
        return ringPassed;
    }





}
