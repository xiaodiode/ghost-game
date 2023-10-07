using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    [SerializeField] private List<LogEntry> logEntries = new(); 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addNewEntry(LogEntry entry){
        logEntries.Add(entry);
    }
}
