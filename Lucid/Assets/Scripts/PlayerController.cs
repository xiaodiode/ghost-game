using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float cameraWidth = 640;

    [SerializeField] public RectTransform playerCanvas;
    [SerializeField] public RectTransform player;
    [SerializeField] public SceneController sceneController;
    [SerializeField] public bool lockMovement;
    [SerializeField] public bool canEnterLeft, canEnterRight;

    [SerializeField] private float playerXMin, playerXMax, playerXCenter;
    [SerializeField] private float playerSpeed;
    [SerializeField] private RectTransform monologueText;
    [SerializeField] private RectTransform narrativeLog;
    [SerializeField] private RectTransform demonInput;

    private Vector3 playerScale, monologueScale, logScale, inputScale;
    private Vector2 move = Vector2.zero;
    private Vector3 newPosition;
    private float verticalInput;
    // Start is called before the first frame update
    void Start()
    {
        playerXMin = player.anchoredPosition.x;
        playerXMax = cameraWidth - playerXMin;
        playerXCenter = cameraWidth/2;

        playerSpeed = sceneController.baseSpeed;

        lockMovement = false;
        canEnterLeft = false;
        canEnterRight= false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("lockmovement: " + lockMovement);
        if(!lockMovement){
            verticalInput = Input.GetAxis("Vertical");
            if((sceneController.isLeftView && verticalInput < 0 && player.localScale.x > 0) ||
                    (sceneController.isLeftView && verticalInput > 0 && player.localScale.x < 0) ||
                    (!sceneController.isLeftView && verticalInput > 0 && player.localScale.x > 0) ||
                    (!sceneController.isLeftView && verticalInput < 0 && player.localScale.x < 0)){
                
                flipPlayer();
            }
            
            horizontalMovement();
        }
        
    }

    private void horizontalMovement(){
        if((sceneController.atLeftEdge && player.anchoredPosition.x!=playerXMin) 
            || (sceneController.atRightEdge && player.anchoredPosition.x!=playerXMax)){
            
            if(verticalInput!=0){
                move.x = verticalInput*playerSpeed*Time.deltaTime;

                if(sceneController.isLeftView){
                    newPosition = player.anchoredPosition + move;    
                }
                else{
                    newPosition = player.anchoredPosition - move; 
                }
                
                newPosition.x = Mathf.Clamp(newPosition.x, playerXMin, playerXMax);

                
                player.anchoredPosition = newPosition;
            }
            
        }
        // check to see if scene and player are at edges of room and set ability to enter room
        if((sceneController.isLeftView && sceneController.atLeftEdge && player.anchoredPosition.x == playerXMin) ||
            (!sceneController.isLeftView && sceneController.atRightEdge && player.anchoredPosition.x == playerXMax)){

            canEnterLeft = true;
            Debug.Log("canenterLeft");
        }
        else if((sceneController.isLeftView && sceneController.atRightEdge && player.anchoredPosition.x == playerXMax) ||
            (!sceneController.isLeftView && sceneController.atLeftEdge && player.anchoredPosition.x == playerXMin)){

            canEnterRight = true;
        }
        else{
            canEnterLeft = false;
            canEnterRight = false;
        }
        Debug.Log("canEnterLeft: " + canEnterLeft);
    }

    private void flipPlayer(){
        playerScale = player.localScale;
        playerScale.x = -playerScale.x;

        // flipping player also flips all character ui, so must flip back
        monologueScale = monologueText.localScale;
        monologueScale.x = -monologueScale.x;

        logScale = narrativeLog.localScale;
        logScale.x = -logScale.x;

        inputScale = demonInput.localScale;
        inputScale.x = -inputScale.x;

        // update all scales to flip 
        player.transform.localScale = playerScale;
        monologueText.localScale = monologueScale;
        narrativeLog.localScale = logScale;
        demonInput.localScale = inputScale;
    }

    private void OnGoLeftRoom(){
        if(canEnterLeft){
            Vector2 newCameraPos = playerCanvas.anchoredPosition;

            float newCameraY, newCameraX;
            
            if(sceneController.isLeftView){
                sceneController.currRoom.leftRoom.leftView.shiftLayersLeft();
                sceneController.currRoom.leftRoom.rightView.shiftLayersRight();

                newCameraY = sceneController.currRoom.leftRoom.leftView.yCameraPosition;
            }
            else{
                sceneController.currRoom.leftRoom.rightView.shiftLayersLeft();
                sceneController.currRoom.leftRoom.leftView.shiftLayersRight();

                newCameraY = sceneController.currRoom.leftRoom.rightView.yCameraPosition;
            }
             
            newCameraX = sceneController.playerEndPos;

            sceneController.switchRooms(true);
        }
    }

    private void OnGoRightRoom(){
        
    }

    private void OnSwitchViews(){   
        Vector2 newCameraPos = playerCanvas.anchoredPosition;
        Vector2 currPlayerPos = player.anchoredPosition;

        float newCameraY = sceneController.otherView.yCameraPosition;
        float newPlayerX;
        
        newCameraPos.y = newCameraY;

        playerCanvas.anchoredPosition = newCameraPos;
        flipPlayer();

        if(currPlayerPos.x < playerXCenter){
            newPlayerX = playerXCenter + (playerXCenter - currPlayerPos.x);
        }
        else{
            newPlayerX = playerXCenter - (currPlayerPos.x - playerXCenter);
        }

        currPlayerPos.x = newPlayerX;
        player.anchoredPosition = currPlayerPos;

        sceneController.switchViews();
    }
}
