using System.Collections;
using UnityEngine;
using TMPro;

public class MonologueController : MonoBehaviour
{

    [Header("Monologue File")]
    [SerializeField] private TextAsset testing;

    [Header("Monologue Appearance")]
    [SerializeField] private TextMeshProUGUI monologueText;
    [SerializeField] private float lineDiffPercent;
    [SerializeField] private Timestamp timestamp;

    [Header("Message Log")]
    [SerializeField] private LogController logController;
    [SerializeField] private LogEntry newEntry;

    [Header("Printing Animation")]
    [SerializeField] private float clearTime;
    [SerializeField] private float printSpeed;


    [Header("Cursor")]
    [SerializeField] private string cursorFont;
    [SerializeField] private string cursorSizePercent;
    [SerializeField] private float cursorBlinkSpeed;
    [SerializeField] private bool cursorBold;
    [SerializeField] private float cursorPrintDelay;

    [Header("Mouse Control")]
    [SerializeField] private MouseController mouse;
    

    private string[] fileLines;
    private int currLineIndex;
    private bool isReset, isFinished, finishPrinting;
    private string currTimeStamp;
    private string lineHeightText;
    private string cursorText, cursorAdjustText;
    private string textWithCursor, textWithoutCursor;
    private bool showCursor;

    
    // Start is called before the first frame update
    void Start()
    {
        completeReset();

        new WaitForSeconds(clearTime);
        StartCoroutine(printMainMonologue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator printMainMonologue(){
        StartCoroutine(resetMonologue());

        while(!isReset){
            yield return null;
        }

        StartCoroutine(printToMonologue(fileLines[currLineIndex]));

        while(!isFinished){
            yield return null;
        }
        
        //removes newline character so cursor can show properly
        monologueText.text = monologueText.text.Remove(monologueText.text.Length - 1, 1);
        
        currLineIndex++;

        StartCoroutine(printCursor());
    }

    private IEnumerator resetMonologue(){
        monologueText.text = cursorAdjustText;
        isFinished = false;
        yield return new WaitForSeconds(clearTime);

        isReset = true;
    }

    private IEnumerator printToMonologue(string toPrint){
        isReset = false;
        mouse.lockMouse(true);
        foreach(char character in toPrint){
            monologueText.text += character;
            
            if(finishPrinting){
                monologueText.text = cursorAdjustText + toPrint;
                break;
            }
            else{
                yield return new WaitForSeconds(printSpeed);
            }
        }

        finishPrinting = false;
        isFinished = true;
        showCursor = true;
    }

    private IEnumerator printCursor(){
        textWithCursor = monologueText.text + cursorText;
        textWithoutCursor = monologueText.text;
        
        yield return new WaitForSeconds(cursorPrintDelay);

        mouse.lockMouse(false);
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
            
            if(!isFinished){
                finishPrinting = true;
            }
            else if(currLineIndex < fileLines.Length){
                currTimeStamp = timestamp.getTimestamp();
                
                newEntry.setTimestamp(currTimeStamp);
                newEntry.setMonologue(textWithoutCursor.Replace("<size=0%>", ""));

                logController.addNewEntry(newEntry);

                finishPrinting = false;
                StartCoroutine(printMainMonologue());
            }
        }
        
    }

    public IEnumerator interjectMonologue(string interjection){
        Debug.Log("interjecting; isFinished: " + isFinished);
        
        if(isFinished){
            completeReset();

            StartCoroutine(resetMonologue());

            while(!isReset){
                yield return null;
            }

            StartCoroutine(printToMonologue(interjection));

            while(!isFinished){
                yield return null;
            }

            StartCoroutine(printCursor());
        }

    }

    private void completeReset(){
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

        isFinished = false;
        finishPrinting = false;
        isReset = false;

        fileLines = testing.text.Split('\n');
        currLineIndex = 0;
    }

    
}
