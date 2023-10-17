using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DataLoader data;

    [Header("Monologue")]
    [SerializeField] private MonologueController monologue;
    [SerializeField] private List<string> baseMonologue = new();

    [Header("Plant Information")]
    [SerializeField] private string plantName;
    [SerializeField] private List<string> description = new();

    [Header("Plant Tampering")]
    [SerializeField] private SpriteRenderer deadState;
    [SerializeField] private SpriteRenderer aliveState;
    [SerializeField] private bool deadOverlay, aliveOverlay, changeSprite;
    [SerializeField] public bool isAlive;

    int randomNum;
    string plantMonologue;

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
        baseMonologue = data.basePlantMonologue;
        description = data.plantDescriptions[plantName];
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
        plantMonologue = plantName + "... " + getRandomMonologue();
        StartCoroutine(monologue.interjectMonologue(plantMonologue));
    }
    public string getRandomMonologue(){
        if(isAlive){
            randomNum = getRandomNum(description.Count);
            return description[randomNum];
        }
        else{
            randomNum = getRandomNum(baseMonologue.Count);
            return baseMonologue[randomNum];
        }
        
    }
    private int getRandomNum(int max){

        return Random.Range(0, max);
    }
}
