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
    [SerializeField] private TextMeshProUGUI monologueText;

    private Vector3 playerScale, monologueScale;
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

                monologueScale = monologueText.rectTransform.localScale;
                monologueScale.x = -monologueScale.x;

                player.transform.localScale = playerScale;
                monologueText.rectTransform.localScale = monologueScale;
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
