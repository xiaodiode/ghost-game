using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class Timestamp : MonoBehaviour
{
    [SerializeField] private GameObject hourHand, minuteHand;

    [SerializeField] private TextMeshProUGUI timestamp;
    private int hour, minute;
    private string leadingZeroH, leadingZeroM;
    // Start is called before the first frame update
    void Start()
    {

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

        if(Mathf.FloorToInt((360 - hourHand.transform.rotation.eulerAngles.z)/30) == 0){
            hour = 12;
        }
        else{
            hour = Mathf.FloorToInt((360 - hourHand.transform.rotation.eulerAngles.z)/30);
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
    }
}
