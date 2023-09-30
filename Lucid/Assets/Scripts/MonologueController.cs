using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MonologueController : MonoBehaviour
{
    [SerializeField] private TextAsset testing;
    [SerializeField] private TextMeshProUGUI monologueText;
    [SerializeField] private Timestamp timestamp;
    [SerializeField] private float clearTime;
    [SerializeField] private float printSpeed = 0.05f;

    private string[] fileLines;
    private int currLineIndex;
    private bool lineFinished, finishPrinting;
    private string currTimeStamp;
    // Start is called before the first frame update
    void Start()
    {
        lineFinished = false;
        finishPrinting = false;

        fileLines = testing.text.Split('\n');
        currLineIndex = 0;

        new WaitForSeconds(1);
        StartCoroutine(printMonologue(testing));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator printMonologue(TextAsset text){
        while(!timestamp.ready){
            yield return null;
        }

        monologueText.text = "";
        lineFinished = false;
        yield return new WaitForSeconds(clearTime);

        currTimeStamp = timestamp.getTimestamp() + "\t";
        monologueText.text = currTimeStamp;
        foreach(char character in fileLines[currLineIndex]){
            monologueText.text += character;
            if(finishPrinting){
                monologueText.text = currTimeStamp + fileLines[currLineIndex];
                break;
            }
            else{
                yield return new WaitForSeconds(printSpeed);
            }
            
        }
        finishPrinting = false;
        lineFinished = true;
        currLineIndex++;
    }

    private void OnNextLine(){
        if(!lineFinished){
            finishPrinting = true;
        }
        else if(currLineIndex < fileLines.Length){
            finishPrinting = false;
            StartCoroutine(printMonologue(testing));
        }
        
    }
}
