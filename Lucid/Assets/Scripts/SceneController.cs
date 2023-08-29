using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Canvas[] sceneLayers;
    public PlayerController player;
    public float[] sceneXMax;
    private float[] sceneSpeed;
    private float baseSpeed = 20f;
    private float baseTime;
    // private float cameraXMax = -67.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        baseTime = sceneXMax[sceneXMax.Length-1]/baseSpeed;
        for(int i=0; i<sceneLayers.Length; i++){
            sceneSpeed[i] = sceneXMax[i]/baseTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sceneMovement();
    }
    private void sceneMovement(){
        float horizontalInput = Input.GetAxis("Horizontal");

        if(horizontalInput!=0 && transform.position.x>=playerXMin && transform.position.x<=playerXMax){
            Vector3 move = new Vector3(horizontalInput, 0f, 0f)*playerSpeed*Time.deltaTime;
            Vector3 newPosition = transform.position + move;
            newPosition.x = Mathf.Clamp(newPosition.x, playerXMin, playerXMax);
            
            transform.position = newPosition;

            if(transform.position.x == playerXMin || transform.position.x == playerXMax){
                cameraOffset = transform.position - playerCamera.transform.position;
                // Debug.Log("changed: cameraOffset: " + cameraOffset);
            }

            updateCamera();
        }
        
    }
    private void updateCamera(){
        if(playerCamera.transform.position.x >= cameraXMin && playerCamera.transform.position.x <= cameraXMax){
            Vector3 newPosition = gameObject.transform.position - cameraOffset;
            newPosition.x = Mathf.Clamp(newPosition.x, cameraXMin, cameraXMax);

            playerCamera.transform.position = newPosition;
        }
    }
}
