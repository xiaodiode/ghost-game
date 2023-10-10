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
    [SerializeField] public string vPlantNameMonologue, vPlantDescrMonologue;
    [SerializeField] public Dictionary<string, string> plantDescriptions = new();

    StringReader fileReader;
    string fileLine, dataSection;

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
        string plantName, plantDescription;
        int endNameIndex;

        fileReader = new StringReader(plantDataText.text);

        while((fileLine = fileReader.ReadLine()) != null){
            fileLine = fileLine.Trim();

            if(fileLine == "baseMonologue" || 
                fileLine == "vibrantMonologue" || 
                fileLine == "plantInfo"){

                    dataSection = fileLine;
                    Debug.Log("dataSection: " + dataSection);
            }
            else if(dataSection == "baseMonologue"){
                basePlantMonologue.Add(fileLine);
            }

            else if(dataSection == "vibrantMonologue"){
                if(vPlantNameMonologue == ""){
                    vPlantNameMonologue = fileLine;
                }
                else{
                    vPlantDescrMonologue = fileLine;
                }
            }

            else if(dataSection == "plantInfo"){
                endNameIndex = fileLine.IndexOf("description");

                plantName = fileLine[..endNameIndex];
                Debug.Log("plantName: " + plantName);
                plantDescription = fileLine[(endNameIndex + "description ".Length)..];
                Debug.Log("plantDescription: " + plantDescription);

                plantDescriptions.Add(plantName, plantDescription);
            }
        }
    }
}
