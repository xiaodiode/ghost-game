using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [Header("Text Files")]
    [SerializeField] private TextAsset demonListText;
    [SerializeField] private TextAsset plantDataText;

    [Header("Data Structures")]

    [Space(10)]
    [Header("Demons")]
    [SerializeField] public List<string> demonList = new();

    [Header("Plants")]
    [SerializeField] public bool plantDataReady = false;
    [SerializeField] public List<string> basePlantMonologue = new();
    [SerializeField] public Dictionary<string, List<string>> plantDescriptions = new();

    [Header("Metal")]
    [SerializeField] public bool metalDataReady = false;
    [SerializeField] public Dictionary<string, List<string>> metalComments;
    [SerializeField] public Dictionary<string, List<string>> goldComments;
    
    string objectName = "";
    List<string> comments = new();


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
                basePlantMonologue.Add(fileLine);
            }

            else if(dataSection == "plantInfo"){
                if(fileLine.Contains("newPlant")){
                    if(comments.Count != 0){
                        plantDescriptions.Add(objectName, comments);
                        comments.Clear();
                    }
                    objectName = fileLine.Replace("newPlant", "").Trim();
                    // Debug.Log("plantName: " + plantName);
                }
                else{
                    comments.Add(fileLine);
                    // Debug.Log("description: " + fileLine);
                    
                }
            }
        }

        plantDataReady = true;
    }

    private void parseMetalData(){
        fileReader = new StringReader(plantDataText.text);

        while((fileLine = fileReader.ReadLine()) != null){
            fileLine = fileLine.Trim();

            if(fileLine == "Gold" || 
                fileLine.Contains("newMetal")){

                    dataSection = fileLine;
            }

            else if(dataSection == "Gold"){
                metalComments.Add(objectName, comments);
                comments.Clear();

                comments.Add(fileLine);
            }

            else{
                if(fileLine.Contains("newMetal")){
                    if(comments.Count != 0){
                        goldComments.Add(objectName, comments);
                        comments.Clear();
                    }
                    objectName = fileLine.Replace("newMetal", "").Trim();
                    // Debug.Log("plantName: " + plantName);
                }
                else{
                    comments.Add(fileLine);
                    // Debug.Log("description: " + fileLine);
                    
                }
            }
        }

        metalDataReady = true;
    }
}
