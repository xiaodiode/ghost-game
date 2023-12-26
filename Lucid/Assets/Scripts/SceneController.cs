using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] public View currView;
    [SerializeField] public bool atLeftEdge, atRightEdge;
    [SerializeField] public bool lockMovement;
    [SerializeField] public bool isMoving;

    [SerializeField] private float baseSpeed;
    [SerializeField] private float botSpeed, midSpeed, topSpeed;
    
    private Vector2 move = Vector2.zero;
    private Vector2 newPosition;
    private float baseTime;
    private float horizontalInput;
    private const float cameraWidth = 640;

    [SerializeField] private float startPosition, endPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        atLeftEdge = false; 
        atRightEdge = false;

        lockMovement = true;

        StartCoroutine(waitForScenes());
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!lockMovement){
           updateSceneMovement(); 
        }
        
    }

    private IEnumerator waitForScenes(){
        while(!currView.ready){
            yield return null;
        }

        baseTime = currView.roomWidth / baseSpeed;

        botSpeed = currView.botWidth / baseTime;
        midSpeed = currView.midWidth / baseTime;
        topSpeed = currView.topWidth / baseTime;

        startPosition = currView.wallLayer.anchoredPosition.x;
        endPosition = startPosition - currView.roomWidth;

        lockMovement = false;
    }

    private void updateSceneMovement(){
        horizontalInput = Input.GetAxis("Horizontal");
        
        if(currView.wallLayer.anchoredPosition.x == endPosition && horizontalInput>0){
            atRightEdge = true;
            isMoving = false;
            Debug.Log("at right edge");
        }
        else if(currView.wallLayer.anchoredPosition.x == startPosition && horizontalInput<0){
            atLeftEdge = true;
            isMoving = false;
            Debug.Log("at left edge");
        }
        else{
            atRightEdge = false;
            atLeftEdge = false;

            if(horizontalInput!=0){
                isMoving = true;

                updateLayerPosition(baseSpeed, currView.wallLayer, currView.roomWidth);
                updateLayerPosition(botSpeed, currView.botLayer, currView.botWidth);
                updateLayerPosition(midSpeed, currView.midLayer, currView.midWidth);
                updateLayerPosition(topSpeed, currView.topLayer, currView.topWidth);
                
            }
            else{
                isMoving = false;
            }
        }
    }

    private void updateLayerPosition(float layerSpeed, RectTransform layer, float layerWidth){
        move.x = -horizontalInput*layerSpeed*Time.deltaTime;
        // Debug.Log("sceneSpeed: " + sceneSpeed[i]);
        newPosition = layer.anchoredPosition + move;
        // Debug.Log("anchoredPosition for " + i + ": " + sceneLayers[i].GetComponent<RectTransform>().anchoredPosition); 
        newPosition.x = Mathf.Clamp(newPosition.x, -layerWidth/2 + cameraWidth, layerWidth/2);
    
        layer.anchoredPosition = newPosition;
    }
}
