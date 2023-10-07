using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LogEntry : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI timestampEntry;
    [SerializeField] public TextMeshProUGUI monologueEntry;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTimestamp(string timestamp){
        timestampEntry.text = timestamp;
    }

    public void setMonologue(string monologue){
        monologueEntry.text = monologue;
    }
}
