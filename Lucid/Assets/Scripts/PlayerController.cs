using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SceneController sceneController;
    public bool canMove;
    public Camera playerCamera;
    private Vector3 playerPosition;
    // private Vector3 cameraPosition, cameraOffset;
    // private float cameraXMax = 50;
    // private float cameraXMin = -17.5f;
    private float playerXMax = 12;
    private float playerXMin = -12;
    private float playerSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        playerPosition = transform.position;
        // cameraPosition = playerCamera.transform.position;

        // cameraOffset = playerPosition - cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if((sceneController.atLeftEdge && transform.position.x!=playerXMin) 
            || (sceneController.atRightEdge && transform.position.x!=playerXMax)){
            playerMovement();
        }
        
    }

    private void playerMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput!=0 && transform.position.x>=playerXMin && transform.position.x<=playerXMax){
            Vector3 move = new Vector3(horizontalInput, 0f, 0f)*playerSpeed*Time.deltaTime;
            Vector3 newPosition = transform.position + move;
            newPosition.x = Mathf.Clamp(newPosition.x, playerXMin, playerXMax);
            
            transform.position = newPosition;

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
