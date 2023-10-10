using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DataLoader data;

    [Header("Plant Information")]
    [SerializeField] private string plantName;
    [SerializeField] private string description;

    [Header("Plant Tampering")]
    [SerializeField] private SpriteRenderer baseState;
    [SerializeField] private Sprite vibrantState;
    [SerializeField] public bool isVibrant;

    // Start is called before the first frame update
    void Start()
    {
        isVibrant = false;
        description = data.plantDescriptions[plantName];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string getDescription(){
        return description;
    }

    public void makeVibrate(){
        baseState.sprite = vibrantState;
        isVibrant = true;
    }

    public void onInteraction(){
        if(isVibrant){
            
        }
    }
}
