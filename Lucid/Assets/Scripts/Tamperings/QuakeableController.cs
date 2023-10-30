using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuakeableController : MonoBehaviour
{
    [Header ("Quakeable Objects Within Control")]
    [SerializeField] public List<Quakeable> quakeables = new();
    [SerializeField] public bool isQuaking;
    [SerializeField] public bool quakingComplete;
    
    private int quakingObjects;
    
    public void Start()
    {
        isQuaking = false;
        quakingComplete = false;

        quakingObjects = quakeables.Count;

        quakeObjects();
    }

    public void Update()
    {
        
    }
    
    public void quakeObjects(){
        foreach(Quakeable quakeable in quakeables){
            quakeable.quakeObject();
        }

        isQuaking = true;
    }

    public void updateQuakingStatus(){
        quakingObjects--;

        if(quakingObjects == 0){
            isQuaking = false;
            quakingComplete = true;
        }
    }
    
}
