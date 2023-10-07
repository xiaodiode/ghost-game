using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    [SerializeField] private List<LogEntry> logEntries = new(); 
    [SerializeField] private GameObject logContent;
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

        LogEntry newEntry = Instantiate(entry);
        newEntry.transform.SetParent(transform);
        newEntry.transform.localScale = entry.transform.localScale;
    }
}
