using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDScript : MonoBehaviour
{
    public TMP_Text countdown;
    public GameObject timer;
    public int victory = 0;
    TimerScript timerScript;
    // Start is called before the first frame update
    void Start()
    {
        timerScript = timer.GetComponent<TimerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        timerScript = timer.GetComponent<TimerScript>();
        countdown.text = "Time Remaining: " + timerScript.timer.ToString("000");
        if (victory != 0)
        {
            countdown.text = "You Win!!! :D";
            timerScript.timer = 0;
        }
        else if (timerScript.timer < 0)
        {
            countdown.text = "Out of Time! :(";
        }
    }
}
