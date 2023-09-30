using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject player;

    [SerializeField] private SceneController sceneController;
    [SerializeField] private bool canMove;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float playerXMax;
    [SerializeField] private float playerXMin;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float cameraXMax;
    [SerializeField] private float cameraXMin;

    private Vector3 cameraPosition, cameraOffset;
    private Vector3 playerPosition, playerScale;
    private float horizontalInput;
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
        horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput < 0 && player.transform.localScale.x > 0 ||
                horizontalInput > 0 && player.transform.localScale.x < 0){
            
            playerScale = player.transform.localScale;
            playerScale.x = -playerScale.x;
            player.transform.localScale = playerScale;
        }
        OnHorizontalMovement();
    }

    private void OnHorizontalMovement(){
        Debug.Log("player position x: " + transform.position.x);
        if((sceneController.atLeftEdge && transform.position.x!=playerXMin) 
            || (sceneController.atRightEdge && transform.position.x!=playerXMax)){
            
            
            Debug.Log("horizontal input: " + horizontalInput);
            Debug.Log("player scale x: " + player.transform.localScale.x);

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
    }
    // private void updateCamera(){
    //     if(playerCamera.transform.position.x >= cameraXMin && playerCamera.transform.position.x <= cameraXMax){
    //         Vector3 newPosition = gameObject.transform.position - cameraOffset;
    //         newPosition.x = Mathf.Clamp(newPosition.x, cameraXMin, cameraXMax);

    //         playerCamera.transform.position = newPosition;
    //     }
    // }
}
