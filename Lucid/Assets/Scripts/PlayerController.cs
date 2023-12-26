using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const float cameraWidth = 640;

    [SerializeField] public RectTransform player;
    [SerializeField] public SceneController sceneController;
    [SerializeField] public bool lockMovement;

    [SerializeField] private float playerXMin, playerXMax;
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

        playerSpeed = sceneController.baseSpeed;

        lockMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("lockmovement: " + lockMovement);
        if(!lockMovement){
            verticalInput = Input.GetAxis("Vertical");
            if(verticalInput < 0 && player.localScale.x > 0 ||
                    verticalInput > 0 && player.localScale.x < 0){
                
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
            
            horizontalMovement();
        }
        
    }

    private void horizontalMovement(){
        if((sceneController.atLeftEdge && player.anchoredPosition.x!=playerXMin) 
            || (sceneController.atRightEdge && player.anchoredPosition.x!=playerXMax)){
            
            if(verticalInput!=0){
                move.x = verticalInput*playerSpeed*Time.deltaTime;
                newPosition = player.anchoredPosition + move;
                newPosition.x = Mathf.Clamp(newPosition.x, playerXMin, playerXMax);
                
                player.anchoredPosition = newPosition;
            }
        }
    }

    private void OnViewLeft(){

    }

    private void OnViewRight(){
        
    }
}
