using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    private Text counterText;
    static private float seconds, minutes;
    private bool playing;

    private void Start()
    {
        playing = true;
        counterText = GetComponent<Text>() as Text;
    }

    public void Update()
    {
        if (playing == true)
        {
            minutes = (int)(Time.timeSinceLevelLoad / 60f);
            seconds = (int)(Time.timeSinceLevelLoad % 60f);
            counterText.text = "Zeit: " + minutes.ToString("00") + ":" + seconds.ToString("00");
        }
    }

    public void stopTimer(){
        playing = false;
    }

    public float getMinutes()
    {
        return minutes;
    }

    public float getSeconds()
    {
        return seconds;
    }
}
