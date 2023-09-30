using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    [SerializeField] public string type;
    // [SerializeField] public delegate void triggerings(); 
    // [SerializeField] public List<triggerings> tamperingList = new List<triggerings>();
    [SerializeField] public int tamperingSize;
    [SerializeField] public int triggerInterval;
    [SerializeField] public GhostSpawner ghostSpawner;

    
    public int randomIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        // tamperingSize = tamperingList.Count;

        // triggerInterval = ghostSpawner.totalTriggerTime/tamperingSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getType(){
        return type;
    }

    public abstract IEnumerator startTamperings();

    public abstract void resetTamperings();
}
