using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public GameObject player;

    [SerializeField] private SceneController sceneController;
    [SerializeField] private float playerXMax;
    [SerializeField] private float playerXMin;
    [SerializeField] private float playerSpeed;
    [SerializeField] private TextMeshProUGUI monologueText;

    private Vector3 playerScale;
    private Vector3 monologueScale;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
