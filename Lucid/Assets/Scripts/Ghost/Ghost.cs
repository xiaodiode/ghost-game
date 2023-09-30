using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ghost : MonoBehaviour
{
    [SerializeField] public string type;
    [SerializeField] public delegate void tamperings(); 
    [SerializeField] public List<tamperings> tamperingList = new List<tamperings>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getType(){
        return type;
    }
}
