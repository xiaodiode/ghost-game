using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class MonologueController : MonoBehaviour
{
    public TextAsset testing;
    public TextMeshProUGUI monologueText;
    private float printSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(printMonologue(testing));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator printMonologue(TextAsset text){
        foreach(char character in text.text){
            if(character == '\n'){
                monologueText.text += "\n";
                yield return new WaitForSeconds(1.0f);
            }
            else{
                monologueText.text += character;
                yield return new WaitForSeconds(printSpeed);
            }
        }
    }
}
