using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] public Canvas[] sceneLayers;
    [SerializeField] public bool atLeftEdge, atRightEdge;


    [SerializeField] private float[] sceneXMax;
    [SerializeField] private int depthValue;
    [SerializeField] private float[] sceneSpeed;
    [SerializeField] private float baseSpeed = 20f;
    private float baseTime;
    private float horizontalInput;
    // private float cameraXMax = -67.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        atLeftEdge = false; atRightEdge = false;
        
        sceneSpeed = new float[sceneLayers.Length];
        baseTime = sceneXMax[sceneXMax.Length-1]/baseSpeed;
        for(int i=0; i<sceneLayers.Length; i++){
            sceneSpeed[i] = sceneXMax[i]/baseTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateSceneMovement();
    }
    private void updateSceneMovement(){
        horizontalInput = Input.GetAxis("Horizontal");
        
        if(sceneLayers[0].GetComponent<RectTransform>().anchoredPosition.x == sceneXMax[0]){
            atRightEdge = true;
            //Debug.Log("at right edge");
        }
        else if(sceneLayers[0].GetComponent<RectTransform>().anchoredPosition.x == 0){
            atLeftEdge = true;
            //Debug.Log("at left edge");
        }
        else{
            atRightEdge = false;
            atLeftEdge = false;
        }
        // for(int i=0; i<sceneLayers.Length; i++){
        //     if(sceneLayers[i].GetComponent<RectTransform>().position.x == sceneXMax[i] + depthValue*i &&
        //         sceneLayers[i].GetComponent<RectTransform>().position.x < depthValue*i){
        //             atSceneEdge = false;
        //     }
        //     else{
        //         player.canMove = true;
        //     }
        // }
        if(horizontalInput!=0){
            for(int i=0; i<sceneLayers.Length; i++){
                Vector2 move = new Vector2(-horizontalInput, 0f)*sceneSpeed[i]*Time.deltaTime;
                // Debug.Log("sceneSpeed: " + sceneSpeed[i]);
                Vector2 newPosition = sceneLayers[i].GetComponent<RectTransform>().anchoredPosition + move;
                // Debug.Log("anchoredPosition for " + i + ": " + sceneLayers[i].GetComponent<RectTransform>().anchoredPosition); 
                newPosition.x = Mathf.Clamp(newPosition.x, sceneXMax[i] + depthValue*i, depthValue*i);
            
                sceneLayers[i].GetComponent<RectTransform>().anchoredPosition = newPosition;
            }
            
          

            // if(transform.position.x == playerXMin || transform.position.x == playerXMax){
            //     cameraOffset = transform.position - playerCamera.transform.position;
            //     // Debug.Log("changed: cameraOffset: " + cameraOffset);
            // }

            // updateCamera();
        }
        
    }
    // private void updateCamera(){
    //     if(playerCamera.transform.position.x >= cameraXMin && playerCamera.transform.position.x <= cameraXMax){
    //         Vector3 newPosition = gameObject.transform.position - cameraOffset;
    //         newPosition.x = Mathf.Clamp(newPosition.x, cameraXMin, cameraXMax);

    //         playerCamera.transform.position = newPosition;
    //     }
    // }
}
