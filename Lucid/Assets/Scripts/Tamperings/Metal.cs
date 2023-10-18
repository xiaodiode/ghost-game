using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private DataLoader data;

    [Header("Monologue")]
    [SerializeField] private MonologueController monologue;

    [Header("Metal Information")]
    [SerializeField] private string metalName;
    [SerializeField] private List<string> metalComments = new();
    [SerializeField] private List<string> goldComments = new();

    [Header("Metal Tampering")]
    [SerializeField] public bool isGold;
    [SerializeField] private SpriteRenderer metallicState;
    [SerializeField] private SpriteRenderer goldState;
    [SerializeField] private bool metallicOverlay, goldOverlay, changeSprite;
    

    int randomNum;
    string metalComment;

    // Start is called before the first frame update
    void Start()
    {
        if(isGold){
            makeGold();
        }
        StartCoroutine(waitForData());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator waitForData(){
        while(!data.metalDataReady){
            yield return null;
        }
        metalComments = data.metalComments[metalName];
        goldComments = data.goldComments[metalName];
    }

    public void makeGold(){
        if(metallicOverlay){
            metallicState.enabled = false;
        }
        else if(goldOverlay){
            goldState.enabled = true;
        }
        else if(changeSprite){
            metallicState.enabled = false;
            goldState.enabled = true;
        }
        
        isGold = true;
    }

    public void onInteraction(){
        metalComment = getRandomMonologue();

        StartCoroutine(monologue.interjectMonologue(metalComment));
    }

    public string getRandomMonologue(){
        if(isGold){
            randomNum = getRandomNum(goldComments.Count);
            return goldComments[randomNum];
        }
        else{
            randomNum = getRandomNum(metalComments.Count);
            return metalComments[randomNum];
        }
        
    }

    private int getRandomNum(int max){

        return Random.Range(0, max);
    }
}
