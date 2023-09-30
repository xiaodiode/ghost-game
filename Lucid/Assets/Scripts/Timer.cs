using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI time;
    private string leadingZeroH, leadingZeroM, leadingZeroS;
    private string timeText = "00:00:00";
    private bool countUp = false;
    private int hours, minutes, seconds;
    private int secondsStarted, secondsPassed;
    
    // Start is called before the first frame update
    void Start()
    {
        leadingZeroH = "0";
        leadingZeroM = "0";
        leadingZeroS = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
        if(countUp){
            // secondsPassed = Mathf.FloorToInt(Time.time) - secondsStarted;
            secondsPassed = Mathf.FloorToInt(Time.time) - secondsStarted;
            seconds = secondsPassed % 60;
            minutes = (secondsPassed/60) % 60;
            hours = (secondsPassed/3600) % 60;
            if(hours > 9){
                leadingZeroH = "";
            }
            else{
                leadingZeroH = "0";
            }
            if(minutes > 9){
                leadingZeroM = ":";
            }
            else{
                leadingZeroM = ":0";
            }
            if(seconds > 9){
                leadingZeroS = ":";
            }
            else{
                leadingZeroS = ":0";
            }
            timeText = leadingZeroH + hours.ToString() + leadingZeroM + minutes.ToString() + leadingZeroS + seconds.ToString();
            
        }
        
        time.text = timeText;
    }
    public void startCountUp(){
        secondsStarted = Mathf.FloorToInt(Time.time);
        hours = 0; minutes = 0; seconds = 0;
        countUp = true;
    }
    public void pauseTimer(){
        countUp = false;
    }
    public void resumeTimer(){
        countUp = true;
    }
    public void resetTimer(){
        countUp = false;
        hours = 0; minutes = 0; seconds = 0;
    }
    public int getTime(){
        return Mathf.FloorToInt(Time.time);
    }
    public int getGameTime(){
        return secondsPassed;
    }
}
