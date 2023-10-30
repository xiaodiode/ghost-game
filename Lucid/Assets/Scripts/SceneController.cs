using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] public Canvas[] sceneLayers;
    [SerializeField] public bool atLeftEdge, atRightEdge;
    [SerializeField] public bool lockMovement;
    [SerializeField] public bool isMoving;


    [SerializeField] private float[] sceneXMax;
    [SerializeField] private int depthValue;
    [SerializeField] private float[] sceneSpeed;
    [SerializeField] private float baseSpeed = 20f;
    private float baseTime;
    private float horizontalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        atLeftEdge = false; atRightEdge = false;
        
        sceneSpeed = new float[sceneLayers.Length];
        baseTime = sceneXMax[sceneXMax.Length-1]/baseSpeed;
        for(int i=0; i<sceneLayers.Length; i++){
            sceneSpeed[i] = sceneXMax[i]/baseTime;
        }

        lockMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockMovement){
           updateSceneMovement(); 
        }
        
    }
    private void updateSceneMovement(){
        horizontalInput = Input.GetAxis("Horizontal");
        
        if(sceneLayers[0].GetComponent<RectTransform>().anchoredPosition.x == sceneXMax[0]){
            atRightEdge = true;
            isMoving = false;
            //Debug.Log("at right edge");
        }
        else if(sceneLayers[0].GetComponent<RectTransform>().anchoredPosition.x == 0){
            atLeftEdge = true;
            isMoving = false;
            //Debug.Log("at left edge");
        }
        else{
            atRightEdge = false;
            atLeftEdge = false;
        }

        if(horizontalInput!=0){
            isMoving = true;
            for(int i=0; i<sceneLayers.Length; i++){
                Vector2 move = new Vector2(-horizontalInput, 0f)*sceneSpeed[i]*Time.deltaTime;
                // Debug.Log("sceneSpeed: " + sceneSpeed[i]);
                Vector2 newPosition = sceneLayers[i].GetComponent<RectTransform>().anchoredPosition + move;
                // Debug.Log("anchoredPosition for " + i + ": " + sceneLayers[i].GetComponent<RectTransform>().anchoredPosition); 
                newPosition.x = Mathf.Clamp(newPosition.x, sceneXMax[i] + depthValue*i, depthValue*i);
            
                sceneLayers[i].GetComponent<RectTransform>().anchoredPosition = newPosition;
            }
            
        }
        
    }
}
