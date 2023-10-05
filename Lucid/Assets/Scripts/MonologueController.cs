using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MonologueController : MonoBehaviour
{

    [Header("Monologue File")]
    [SerializeField] private TextAsset testing;

    [Header("Monologue Appearance")]
    [SerializeField] private TextMeshProUGUI monologueText;
    [SerializeField] private float lineDiffPercent;
    [SerializeField] private Timestamp timestamp;


    [Header("Printing Animation")]
    [SerializeField] private float clearTime;
    [SerializeField] private float printSpeed;


    [Header("Cursor")]
    [SerializeField] private string cursorFont;
    [SerializeField] private string cursorSizePercent;
    [SerializeField] private float cursorBlinkSpeed;
    [SerializeField] private bool cursorBold;
    [SerializeField] private float cursorPrintDelay;
    

    private string[] fileLines;
    private int currLineIndex;
    private bool lineFinished, finishPrinting;
    private string currTimeStamp;
    private string lineHeightText;
    private string cursorText, cursorAdjustText;
    private string textWithCursor, textWithoutCursor;
    private bool showCursor;
    
    // Start is called before the first frame update
    void Start()
    {
        showCursor = false;
        cursorText = "<font=\"" + cursorFont + "\">" +"<size=" + cursorSizePercent + "%>";

        if(cursorBold){
            cursorText += "<b>  o";
        }
        else{
            cursorText += "  o";
        }

        lineHeightText = "<line-height=" + lineDiffPercent + "%>";
        cursorAdjustText = "<size=0%>" + lineHeightText + "\n</size>";

        lineFinished = false;
        finishPrinting = false;

        fileLines = testing.text.Split('\n');
        currLineIndex = 0;

        new WaitForSeconds(clearTime);
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
// It's ok. <font="amatic"><size=200%><b>I</font>
        monologueText.text = cursorAdjustText;
        lineFinished = false;
        yield return new WaitForSeconds(clearTime);

        // currTimeStamp = timestamp.getTimestamp() + "\t";
        // monologueText.text += currTimeStamp;
        foreach(char character in fileLines[currLineIndex]){
            monologueText.text += character;
            
            if(finishPrinting){
                monologueText.text = cursorAdjustText + fileLines[currLineIndex];
                break;
            }
            else{
                yield return new WaitForSeconds(printSpeed);
            }

        }
        //removes newline character so cursor can show properly
        monologueText.text = monologueText.text.Remove(monologueText.text.Length - 1, 1);

        finishPrinting = false;
        lineFinished = true;

        currLineIndex++;
        

        showCursor = true;
        StartCoroutine(printCursor());

    }

    private IEnumerator printCursor(){
        textWithCursor = monologueText.text + cursorText;
        textWithoutCursor = monologueText.text;
        
        yield return new WaitForSeconds(cursorPrintDelay);

        while(showCursor){
            if(monologueText.text == textWithoutCursor){
                monologueText.text = textWithCursor;
            }
            else{
                monologueText.text = textWithoutCursor;
            }
            yield return new WaitForSeconds(cursorBlinkSpeed);
        }
    }
    private void OnNextLine(){
        if(currLineIndex != fileLines.Length){
            showCursor = false;
            
            if(!lineFinished){
                finishPrinting = true;
            }
            else if(currLineIndex < fileLines.Length){
                finishPrinting = false;
                StartCoroutine(printMonologue(testing));
            }
        }
        
    }
}
