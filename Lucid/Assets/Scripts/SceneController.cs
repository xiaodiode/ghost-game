using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{   
    const float cameraWidth = 640;

    [SerializeField] public Mansion mansion;
    [SerializeField] public Room currRoom;
    [SerializeField] public View currView;
    [SerializeField] public View otherView;
    [SerializeField] public bool isLeftView;
    [SerializeField] public bool atLeftEdge, atRightEdge;
    [SerializeField] public bool lockMovement;
    [SerializeField] public bool isMoving;
    [SerializeField] public float baseSpeed;
    [SerializeField] public float playerStartPos, playerEndPos;

    [SerializeField] private float botSpeed, midSpeed, topSpeed;
    
    private Vector2 move = Vector2.zero;
    private Vector2 newPosition;
    private float baseTime;
    private float verticalInput;
    

    
    
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

        baseTime = (currView.roomWidth - cameraWidth) / baseSpeed;

        // initializes the layer speeds based on the speed of the room
        botSpeed = (currView.botWidth - cameraWidth) / baseTime;
        midSpeed = (currView.midWidth - cameraWidth) / baseTime;
        topSpeed = (currView.topWidth - cameraWidth) / baseTime;

        lockMovement = false;

        // checks if current view is left/right and initializes the other view of room
        if(currView == currRoom.leftView){
            isLeftView = true;
            otherView = currRoom.rightView;
        }
        else{
            isLeftView = false;
            otherView = currRoom.leftView;
        }

        playerStartPos = currView.wallLayer.anchoredPosition.x;
        playerEndPos = playerStartPos - currView.roomWidth + cameraWidth;

        // Debug.Log("startRightPos: " + startRightPos + " endrightpos: " + endRightPos);
    }

    private void updateSceneMovement(){
        verticalInput = Input.GetAxis("Vertical");
        
        if((isLeftView && currView.wallLayer.anchoredPosition.x == playerEndPos && verticalInput>=0) || 
            (!isLeftView && currView.wallLayer.anchoredPosition.x == playerEndPos && verticalInput<=0)){
            atRightEdge = true;
            isMoving = false;
            Debug.Log("at right edge");
        }
        else if((isLeftView && currView.wallLayer.anchoredPosition.x == playerStartPos && verticalInput<=0) ||
            (!isLeftView && currView.wallLayer.anchoredPosition.x == playerStartPos && verticalInput>=0)){
            atLeftEdge = true;
            isMoving = false;
            Debug.Log("at left edge");
        }
        else{
            atRightEdge = false;
            atLeftEdge = false;

            if(verticalInput!=0){
                isMoving = true;

                // moves the layers of the current room's left and right layers
                updateLayerPosition(currView.isLeft, baseSpeed, currView.wallLayer, currView.roomWidth);
                updateLayerPosition(currView.isLeft, botSpeed, currView.botLayer, currView.botWidth);
                updateLayerPosition(currView.isLeft, midSpeed, currView.midLayer, currView.midWidth);
                updateLayerPosition(currView.isLeft, topSpeed, currView.topLayer, currView.topWidth);

                updateLayerPosition(otherView.isLeft, baseSpeed, otherView.wallLayer, otherView.roomWidth);
                updateLayerPosition(otherView.isLeft, botSpeed, otherView.botLayer, otherView.botWidth);
                updateLayerPosition(otherView.isLeft, midSpeed, otherView.midLayer, otherView.midWidth);
                updateLayerPosition(otherView.isLeft, topSpeed, otherView.topLayer, otherView.topWidth);
                
            }
            else{
                isMoving = false;
            }
        }
    }

    private void updateLayerPosition(bool isLeft, float layerSpeed, RectTransform layer, float layerWidth){
        if(isLeft){
            move.x = -verticalInput*layerSpeed*Time.deltaTime;
            // Debug.Log("sceneSpeed: " + sceneSpeed[i]);
            newPosition = layer.anchoredPosition + move;
            // Debug.Log("anchoredPosition for " + i + ": " + sceneLayers[i].GetComponent<RectTransform>().anchoredPosition); 
            newPosition.x = Mathf.Clamp(newPosition.x, -layerWidth/2 + cameraWidth, layerWidth/2);
        }
        else{
            move.x = -verticalInput*layerSpeed*Time.deltaTime;
            // Debug.Log("sceneSpeed: " + sceneSpeed[i]);
            newPosition = layer.anchoredPosition - move;
            // Debug.Log("anchoredPosition for " + i + ": " + sceneLayers[i].GetComponent<RectTransform>().anchoredPosition); 
            newPosition.x = Mathf.Clamp(newPosition.x, -layerWidth/2 + cameraWidth, layerWidth/2);
        }
    
        layer.anchoredPosition = newPosition;
    }

    // update the view variables
    public void switchViews(){
        View tempView = currView;

        currView = otherView;
        otherView = tempView;

        isLeftView = !isLeftView;
    }

    public void switchRooms(bool isLeft){
        if(isLeft){
            currRoom = currRoom.leftRoom;
            atLeftEdge = false;
            atRightEdge = true;
        }
        else{
            currRoom = currRoom.rightRoom;
            atRightEdge = false;
            atLeftEdge = true;
        }

        if(isLeftView){
            currView = currRoom.leftView;
            otherView = currRoom.rightView;    
        }
        else{
            currView = currRoom.rightView;
            otherView = currRoom.leftView;    
        }

        playerStartPos = currView.wallLayer.anchoredPosition.x;
        playerEndPos = playerStartPos - currView.roomWidth + cameraWidth;
    }

    
}
