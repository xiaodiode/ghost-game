using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timestamp : MonoBehaviour
{
    [SerializeField] public bool ready;

    [SerializeField] private GameObject hourHand, minuteHand;
    [SerializeField] private TextMeshProUGUI timestamp;
    private int hour, minute;
    private int hourOffset;
    private string leadingZeroH, leadingZeroM;
    private bool secondRotation;
    // Start is called before the first frame update
    void Start()
    {
        hour = 0;
        hourOffset = 0;
        secondRotation = false;
        ready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(((360 - minuteHand.transform.rotation.eulerAngles.z)/6)<10 || ((360 - minuteHand.transform.rotation.eulerAngles.z)/6) == 60){
            leadingZeroM = "0";
        }
        else{
            leadingZeroM = "";
        }

        if(timestamp.text == "11:59"){
            secondRotation = true;
        }
        else if(timestamp.text == "23:59"){
            secondRotation = false;
        }

        if(Mathf.FloorToInt((360 - hourHand.transform.rotation.eulerAngles.z)/30) == 0){
            if(!secondRotation){
                hour = 0;
                hourOffset = 0;
            }
            else{
                hour = 12;
                hourOffset = 12;
            }
        }
        else{
            hour = hourOffset + Mathf.FloorToInt((360 - hourHand.transform.rotation.eulerAngles.z)/30);
        }

        if(Mathf.FloorToInt((360 - minuteHand.transform.rotation.eulerAngles.z)/6) == 60){
            minute = 0;
        }
        else{
            minute = Mathf.FloorToInt((360 - minuteHand.transform.rotation.eulerAngles.z)/6);
        }

        if(hour < 10){
            leadingZeroH = "0";
        }
        else{
            leadingZeroH = "";
        }
        timestamp.text = leadingZeroH + hour.ToString() + ":" + leadingZeroM + minute.ToString();
        ready = true;
    }

    public string getTimestamp(){
        return timestamp.text;
    }
}
