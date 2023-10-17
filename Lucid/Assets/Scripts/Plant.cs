using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DataLoader data;

    [Header("Plant Information")]
    [SerializeField] private string plantName;
    [SerializeField] private List<string> description = new();

    [Header("Plant Tampering")]
    [SerializeField] private SpriteRenderer baseState;
    [SerializeField] private Sprite vibrantState;
    [SerializeField] public bool isVibrant;

    int randomNum;

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

    public string getRandomDescr(){
        randomNum = getRandomNum(description.Count);

        return description[randomNum];
    }

    public void makeVibrate(){
        baseState.sprite = vibrantState;
        isVibrant = true;
    }

    public void onInteraction(){
        if(isVibrant){
            
        }
    }

    private int getRandomNum(int max){

        return Random.Range(0, max-1);
    }
}
