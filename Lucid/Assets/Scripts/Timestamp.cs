using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class Timestamp : MonoBehaviour
{
    public ClockRotation hourHand, minuteHand;
    private TextMeshProUGUI timestamp;
    private int hour, minute;
    private string leadingZeroH, leadingZeroM;
    // Start is called before the first frame update
    void Start()
    {
        timestamp = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(((360 - minuteHand.transform.rotation.eulerAngles.z)/6)<10){
            leadingZeroH = "0";
        }
        else{
            leadingZeroH = "";
        }
        
        if(Mathf.FloorToInt((360 - hourHand.transform.rotation.eulerAngles.z)/30) == 0){
            hour = 12;
        }
        else{
            hour = Mathf.FloorToInt((360 - hourHand.transform.rotation.eulerAngles.z)/30);
        }
        timestamp.text = hour.ToString() + ":" 
        + leadingZeroH + Mathf.FloorToInt((360 - minuteHand.transform.rotation.eulerAngles.z)/6).ToString();
    }
}
