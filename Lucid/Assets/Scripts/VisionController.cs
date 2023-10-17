using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionController : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField] private Canvas visionCanvas;
    [SerializeField] private MouseController mouse;

    [Header("Player Movement Settings")]
    [SerializeField] private Canvas playerCanvas;
    [SerializeField] private PlayerController player;
    [SerializeField] private SceneController scene;

    [Header("Zoom Settings")]
    [SerializeField] private float maxZoomOffset;
    [SerializeField] private float zoomSpeed;


    Vector3 minZoomVect, maxZoomVect, newZoomVect, zoomSpeedVect;
    float maxZoomScale;
    float mouseScroll;

    Vector2 movePos;
    public void Start()
    {
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //     parentCanvas.transform as RectTransform, Input.mousePosition,
        //     parentCanvas.worldCamera,
        //     out movePos);

        minZoomVect = visionCanvas.transform.localScale;
        maxZoomVect = new Vector3(minZoomVect.x + maxZoomOffset, minZoomVect.y + maxZoomOffset, minZoomVect.z + maxZoomOffset);
        zoomSpeedVect = new Vector3(zoomSpeed, zoomSpeed, zoomSpeed);

        maxZoomScale = minZoomVect.x + maxZoomOffset;
        
    }

    public void Update()
    {
        if(!mouse.isLocked){
            updateVisionPosition();
            if((mouseScroll = Input.GetAxis("Mouse ScrollWheel")) != 0){
                updateVisionZoom();
            }
        }
        
    }

    private void updateVisionPosition(){
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                visionCanvas.transform as RectTransform,
                Input.mousePosition, visionCanvas.worldCamera,
                out movePos);

            transform.position = visionCanvas.transform.TransformPoint(movePos);
    }

    private void updateVisionZoom(){
        if((visionCanvas.transform.localScale.x + mouseScroll*zoomSpeed) > maxZoomScale){
            newZoomVect = maxZoomVect;
        }
        else if((visionCanvas.transform.localScale.x + mouseScroll*zoomSpeed) < minZoomVect.x){
            newZoomVect = minZoomVect;
            player.lockMovement = false;
            scene.lockMovement = false;
        }
        else{
            newZoomVect = mouseScroll*zoomSpeedVect + visionCanvas.transform.localScale;
            player.lockMovement = true;
            scene.lockMovement = true;
            // playerCanvas.transform.localScale -= mouseScroll*zoomSpeedVect;
        }

        visionCanvas.transform.localScale = newZoomVect;  
    }
}
