using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public PlayerController player;
    private int hours, minutes, seconds;
    private int secondsStarted, secondsPassed;
    private TextMeshProUGUI time;
    private string leadingZeroH, leadingZeroM, leadingZeroS;
    private string timeText = "00:00:00";
    private int totalSeconds = 5;
    bool countUp = false; bool countDown = false;
    public bool levelClear;
    int wasCountingUp = -1;
    // Start is called before the first frame update
    void Start()
    {
        levelClear = false;
        time = transform.GetComponent<TextMeshProUGUI>();
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
        else if(countDown){
            secondsPassed = totalSeconds - (Mathf.FloorToInt(Time.time) - secondsStarted);
            seconds = secondsPassed % 60;
            minutes = (secondsPassed/60) % 60;
            if(minutes > 9){
                leadingZeroM = "";
            }
            else{
                leadingZeroM = "0";
            }
            if(seconds > 9){
                leadingZeroS = ":";
            }
            else{
                leadingZeroS = ":0";
            }
            if(secondsPassed == 0){
                levelClear = true;
                // if(player.selectedLevel == player.levelsUnlocked){
                //     gemController.enableGemPopup("level");
                //     if(player.selectedLevel != 101)
                //         player.levelsUnlocked+=1;
                // }
                // gameManager.gameOver();
                resetTimer();
            }
            timeText = leadingZeroM + minutes.ToString() + leadingZeroS + seconds.ToString();
            
        }
        
        
        
        time.text = timeText;
    }
    public void startCountUp(){
        secondsStarted = Mathf.FloorToInt(Time.time);
        hours = 0; minutes = 0; seconds = 0;
        countUp = true;
    }
    public void startCountDown(){
        secondsStarted = Mathf.FloorToInt(Time.time);
        countDown = true;
    }
    public void pauseTimer(){
        
        if(countUp){
            wasCountingUp = 1;
            countUp = false;
        }
        else if(countDown){
            wasCountingUp = 2;
            countDown = false;
        }
    }
    public void resumeTimer(){
        if(wasCountingUp == 1)
            countUp = true;
        else if(wasCountingUp == 2)
            countDown = true;
    }
    public void resetTimer(){
        countUp = false;
        countDown = false;
        hours = 0; minutes = 0; seconds = 0;
    }
    public int getTime(){
        return Mathf.FloorToInt(Time.time);
    }
    public int getGameTime(){
        return secondsPassed;
    }

    public bool trackTime(float seconds, bool timeReady){
        int startTime = Mathf.FloorToInt(Time.time);
        while((Mathf.FloorToInt(Time.time) - startTime) != seconds){
            timeReady = false;
        }
        timeReady = true;
        return timeReady;
    }
}
