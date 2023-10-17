using System;
using System.Collections.Generic;
using System.IO;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataLoader : MonoBehaviour
{
    [Header("Text Files")]
    [SerializeField] private TextAsset demonListText;
    [SerializeField] private TextAsset plantDataText;
    [SerializeField] private TextAsset metalDataText;

    [Header("Data Structures")]

    [Space(10)]
    [Header("Demons")]
    [SerializeField] public List<string> demonList = new();

    [Header("Plants")]
    [SerializeField] public bool plantDataReady = false;
    [SerializeField] public List<string> deadPlantComments = new();
    [SerializeField] public Dictionary<string, List<string>> alivePlantComments = new();

    [Header("Metal")]
    [SerializeField] public bool metalDataReady = false;
    [SerializeField] public Dictionary<string, List<string>> metalComments = new();
    [SerializeField] public Dictionary<string, List<string>> goldComments = new();
    
    string objectName = "";
    List<string> comments = new();
    bool newHeading = true;


    StringReader fileReader;
    string fileLine, dataSection;
    

    // Start is called before the first frame update
    void Start()
    {
        parseDemonList();
        parsePlantData();
        parseMetalData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void parseDemonList(){
        fileReader = new StringReader(demonListText.text);
        
        fileLine = fileReader.ReadLine();
        while(fileLine != null){
            demonList.Add(fileLine.Trim());

            fileLine = fileReader.ReadLine();
        }
    }

    private void parsePlantData(){

        fileReader = new StringReader(plantDataText.text);

        while((fileLine = fileReader.ReadLine()) != null){
            fileLine = fileLine.Trim();

            if(fileLine == "baseMonologue" || 
                fileLine == "plantInfo"){

                    dataSection = fileLine;
                    // Debug.Log("dataSection: " + dataSection);
            }
            else if(dataSection == "baseMonologue"){
                deadPlantComments.Add(fileLine);
            }

            else if(dataSection == "plantInfo"){
                if(fileLine.Contains("newPlant")){
                    if(comments.Count != 0){
                        alivePlantComments.Add(objectName, comments);
                        comments = new();
                    }
                    objectName = fileLine.Replace("newPlant", "").Trim();
                    // Debug.Log("plantName: " + plantName);
                }
                else{
                    comments.Add(fileLine);
                }
            }
        }

        alivePlantComments.Add(objectName, comments);

        // debugDictionary(true, alivePlantComments, null);

        plantDataReady = true;
    }

    private void parseMetalData(){
        fileReader = new StringReader(metalDataText.text);
        comments = new();

        while((fileLine = fileReader.ReadLine()) != null){
            fileLine = fileLine.Trim();

            if(fileLine == "newMetal" || fileLine == "Nongold" || fileLine == "Gold"){
                    dataSection = fileLine;
                    newHeading = true;
                    // Debug.Log("dataSection: " + dataSection);
            }

            else if(dataSection == "newMetal"){
                if(comments.Count != 0){
                    // Debug.Log("under metal objectName: " + objectName);
                    goldComments.Add(objectName, comments);
                    comments = new();
                }
                objectName = fileLine;
            }

            else if(dataSection == "Nongold"){
                comments.Add(fileLine);
                // Debug.Log("description: " + fileLine);
                
            }

            else if(dataSection == "Gold"){
                if(newHeading){
                    newHeading = false;
                    // Debug.Log("under gold objectName: " + objectName);
                    metalComments.Add(objectName, comments);
                    comments = new();
                }
                comments.Add(fileLine);
            }
        }


        goldComments.Add(objectName, comments);

        // debugDictionary(true, metalComments, null);
        // debugDictionary(true, goldComments, null);

        metalDataReady = true;
    }

    private void debugDictionary(bool valIsList, Dictionary<string, List<string>> dictWithList, Dictionary<string, string> dictWithString){
        if(valIsList){
            foreach(KeyValuePair<string, List<string>> pair in dictWithList){
                foreach(string val in pair.Value){
                    Debug.Log("val name: " + pair.Key + " | comment: " + val);
                }
            }
        }
        else{
            foreach(KeyValuePair<string, string> pair in dictWithString){
                Debug.Log("val name: " + pair.Key + " | comment: " + pair.Value);
            }
        }

    }
}
