using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public SceneController sceneController;
    [SerializeField] public bool lockMovement;

    [SerializeField] private float playerXMax;
    [SerializeField] private float playerXMin;
    [SerializeField] private float playerSpeed;
    [SerializeField] private RectTransform monologueText;
    [SerializeField] private RectTransform narrativeLog;
    [SerializeField] private RectTransform demonInput;

    private Vector3 playerScale, monologueScale, logScale, inputScale;
    private Vector3 move = Vector3.zero;
    private Vector3 newPosition;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        lockMovement = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("lockmovement: " + lockMovement);
        if(!lockMovement){
            horizontalInput = Input.GetAxis("Horizontal");
            if(horizontalInput < 0 && player.transform.localScale.x > 0 ||
                    horizontalInput > 0 && player.transform.localScale.x < 0){
                
                playerScale = player.transform.localScale;
                playerScale.x = -playerScale.x;

                monologueScale = monologueText.localScale;
                monologueScale.x = -monologueScale.x;

                logScale = narrativeLog.localScale;
                logScale.x = -logScale.x;

                inputScale = demonInput.localScale;
                inputScale.x = -inputScale.x;

                player.transform.localScale = playerScale;
                monologueText.localScale = monologueScale;
                narrativeLog.localScale = logScale;
                demonInput.localScale = inputScale;
            }
            
            horizontalMovement();
        }
        
    }

    private void horizontalMovement(){
        if((sceneController.atLeftEdge && transform.position.x!=playerXMin) 
            || (sceneController.atRightEdge && transform.position.x!=playerXMax)){
            
            if(horizontalInput!=0){
                move.x = horizontalInput*playerSpeed*Time.deltaTime;
                newPosition = transform.position + move;
                newPosition.x = Mathf.Clamp(newPosition.x, playerXMin, playerXMax);
                
                transform.position = newPosition;
            }
        }
    }
}
