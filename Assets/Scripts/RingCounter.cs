using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RingCounter : MonoBehaviour {
    public Text ringCounter;
    private int anzahlRinge = 0;
    private int gesamtAnzahl;

    // Use this for initialization
    void Start () {
        ringCounter = GetComponent<Text>() as Text;
    }
	
	// Update is called once per frame
	public void Update () {
        ringCounter.text = "Ringe: " + anzahlRinge +" / " + gesamtAnzahl;
    }

    public void countRings(int gesamt){
        anzahlRinge++;
        gesamtAnzahl = gesamt;
    }
}
