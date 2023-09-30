using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [Header("Current Ghosts")]
    [SerializeField] public List<Ghost> allActiveGhosts = new();
    [SerializeField] public int totalTriggerTime;

    [Header("Ghost Types")]
    [SerializeField] private List<Ghost> allSubtleTypes = new();
    [SerializeField] private List<Ghost> allUnsubtleTypes = new();

    [Header("Round 1")]
    [SerializeField] private int firstSpawnTime;
    [SerializeField] private int firstSpawnSubtleNum; 
    [SerializeField] private int firstSpawnUnsubtleNum;

    [Header("Round 2")]
    [SerializeField] private int secondSpawnTime;
    [SerializeField] private int secondSpawnSubtleNum; 
    [SerializeField] private int secondSpawnUnsubtleNum;

    [Header("Round 3")]
    [SerializeField] private int thirdSpawnTime;
    [SerializeField] private int thirdSpawnSubtleNum; 
    [SerializeField] private int thirdSpawnUnsubtleNum;

    private List<Ghost> toTrigger = new();
    private int totalSubtleTypes, totalUnsubtleTypes;
    
    int randomIndex;
    int secondsStart, secondsPassed;

  
    void Start()
    {
        secondsStart = Mathf.FloorToInt(Time.time);

        totalSubtleTypes = allSubtleTypes.Count;
        totalUnsubtleTypes = allUnsubtleTypes.Count;
    }

    // Update is called once per frame
    void Update()
    {
        secondsPassed = Mathf.FloorToInt(Time.time) - secondsStart;
        
        checkSpawnTime(firstSpawnTime, firstSpawnSubtleNum, firstSpawnUnsubtleNum);
        checkSpawnTime(secondSpawnTime, secondSpawnSubtleNum, secondSpawnUnsubtleNum);
        checkSpawnTime(thirdSpawnTime, thirdSpawnSubtleNum, thirdSpawnUnsubtleNum);
        
    }

    private void checkSpawnTime(int spawnTime, int spawnSubtleNum, int spawnUnsubtleNum){
        if(secondsPassed == spawnTime){
            for(int i=0; i<spawnSubtleNum; i++){
                randomIndex = Mathf.FloorToInt(Random.Range(0, totalSubtleTypes - 1));
                allActiveGhosts.Add(allSubtleTypes[randomIndex]);
                toTrigger.Add(allSubtleTypes[randomIndex]);

                allSubtleTypes.RemoveAt(randomIndex);
                totalSubtleTypes--;
            }
            for(int i=0; i<spawnUnsubtleNum; i++){
                randomIndex = Mathf.FloorToInt(Random.Range(0, totalUnsubtleTypes - 1));
                allActiveGhosts.Add(allUnsubtleTypes[randomIndex]);
                toTrigger.Add(allUnsubtleTypes[randomIndex]);

                allUnsubtleTypes.RemoveAt(randomIndex);
                totalUnsubtleTypes--;
            }
            triggerGhosts();
        }
    }

    private void triggerGhosts(){
        for(int i=0; i<toTrigger.Count; i++){
            StartCoroutine(toTrigger[i].startTamperings());
        }
        toTrigger.Clear();
    }

    public void resetGhostSpawner(){
        foreach(Ghost ghost in allActiveGhosts){
            if(ghost.type == "subtle"){
                allSubtleTypes.Add(ghost);
            }
            else if(ghost.type == "unsubtle"){
                allUnsubtleTypes.Add(ghost);
            }
        }
        totalSubtleTypes = allSubtleTypes.Count;
        totalUnsubtleTypes = allUnsubtleTypes.Count;

        toTrigger.Clear();

        secondsStart = Mathf.FloorToInt(Time.time);
    }
}
