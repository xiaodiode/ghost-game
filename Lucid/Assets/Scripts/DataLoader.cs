using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [Header("Text Files")]
    [SerializeField] private TextAsset demonListText;

    [Header("Data Structures")]
    [SerializeField] public List<string> demonList = new();

    // Start is called before the first frame update
    void Start()
    {
        parseDemonList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void parseDemonList(){
        StringReader textReader = new StringReader(demonListText.text);
        
        string line = textReader.ReadLine();
        while(line != null){
            demonList.Add(line.Trim());

            line = textReader.ReadLine();
        }
    }
}
