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
    [SerializeField] public List<string> basePlantMonologue = new();
    [SerializeField] public Dictionary<string, List<string>> plantDescriptions = new();
    [SerializeField] public bool plantDataReady = false;

    StringReader fileReader;
    string fileLine, dataSection;
    List<string> plantDescr = new();

    // Start is called before the first frame update
    void Start()
    {
        parseDemonList();
        parsePlantData();
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
        string plantName = "";

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
                    if(plantDescr.Count != 0){
                        plantDescriptions.Add(plantName, plantDescr);
                        plantDescr.Clear();
                    }
                    plantName = fileLine.Replace("newPlant", "").Trim();
                    // Debug.Log("plantName: " + plantName);
                }
                else{
                    plantDescr.Add(fileLine);
                    // Debug.Log("description: " + fileLine);
                    
                }
            }
        }

        plantDataReady = true;
    }
}
