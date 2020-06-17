using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalScene : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;
    private float fadeInDuration = 2;
    private bool gameStarted;

    //Fade in game from play button
    private bool goingBack = false;

    public Timer myTimer;
    public Text playingTime;

    // Use this for initialization
    private void Start()
    {
        //grap the only canvasGroup in the scene
        fadeGroup = FindObjectOfType<CanvasGroup>();

        //start with a white screen
        fadeGroup.alpha = 1;

        //Timer anzeigen
        myTimer = GetComponent<Timer>();
        playingTime = GetComponent<Text>() as Text;
        playingTime.text = "Zeit: " + myTimer.getMinutes().ToString("00") + ":" + myTimer.getSeconds().ToString("00");
    }

    //Update is called once per frame
    private void Update()
    {
        //fade in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
        //entering level zoom
        if (goingBack)
        {

            //Enter the level
            SceneManager.LoadScene("Menu");

        }
    }

    public void OnClick()
    {
        goingBack = true;
        Debug.Log("back clicked");
    }
}
