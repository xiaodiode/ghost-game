using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DataLoader data;

    [Header("Monologue")]
    [SerializeField] private MonologueController monologue;
    

    [Header("Plant Information")]
    [SerializeField] private string plantName;
    [SerializeField] private List<string> deadComments = new();
    [SerializeField] private List<string> aliveComments = new();

    [Header("Plant Tampering")]
    [SerializeField] public bool isAlive;
    [SerializeField] private SpriteRenderer deadState;
    [SerializeField] private SpriteRenderer aliveState;
    [SerializeField] private bool deadOverlay, aliveOverlay, changeSprite;
    

    int randomNum;
    string plantComment;

    // Start is called before the first frame update
    void Start()
    {
        if(isAlive){
            makeAlive();
        }
        StartCoroutine(waitForData());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator waitForData(){
        while(!data.plantDataReady){
            yield return null;
        }
        deadComments = data.deadPlantComments;
        aliveComments = data.alivePlantComments[plantName];
    }

    public void makeAlive(){
        if(deadOverlay){
            deadState.enabled = false;
        }
        else if(aliveOverlay){
            aliveState.enabled = true;
        }
        else if(changeSprite){
            deadState.sprite = aliveState.sprite;
        }
        
        isAlive = true;
    }

    public void onInteraction(){
        Debug.Log("pressing button");
        plantComment = plantName + "... " + getRandomMonologue();
        StartCoroutine(monologue.interjectMonologue(plantComment));
    }

    public string getRandomMonologue(){
        if(isAlive){
            randomNum = getRandomNum(aliveComments.Count);
            return aliveComments[randomNum];
        }
        else{
            randomNum = getRandomNum(deadComments.Count);
            return deadComments[randomNum];
        }
        
    }
    
    private int getRandomNum(int max){

        return Random.Range(0, max);
    }
}
