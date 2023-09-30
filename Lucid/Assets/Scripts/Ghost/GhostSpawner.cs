using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [Header("Ghost Types")]
    [SerializeField] private List<Ghost> allSubtleTypes = new();
    [SerializeField] private List<Ghost> allUnSubtleTypes = new();

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
