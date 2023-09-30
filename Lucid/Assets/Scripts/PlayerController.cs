using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject player;

    [SerializeField] private SceneController sceneController;
    [SerializeField] private float playerXMax;
    [SerializeField] private float playerXMin;
    [SerializeField] private float playerSpeed;

    private Vector3 cameraPosition, cameraOffset;
    private Vector3 playerPosition, playerScale;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        playerPosition = transform.position;
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
        horizontalMovement();
    }

    private void horizontalMovement(){
        if((sceneController.atLeftEdge && transform.position.x!=playerXMin) 
            || (sceneController.atRightEdge && transform.position.x!=playerXMax)){
            
            if(horizontalInput!=0 && transform.position.x>=playerXMin && transform.position.x<=playerXMax){
                Vector3 move = new Vector3(horizontalInput, 0f, 0f)*playerSpeed*Time.deltaTime;
                Vector3 newPosition = transform.position + move;
                newPosition.x = Mathf.Clamp(newPosition.x, playerXMin, playerXMax);
                
                transform.position = newPosition;
            }
        }
    }
}
