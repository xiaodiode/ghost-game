using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisionController : MonoBehaviour
{
    [SerializeField] private Canvas playerCanvas;
    [SerializeField] private Canvas visionCanvas;
    [SerializeField] private MouseController mouse;

    [Header("Zoom Settings")]
    [SerializeField] private Camera zoomCamera;
    [SerializeField] private float maxIncrZoom, minZoom;
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
        maxZoomVect = new Vector3(minZoomVect.x + maxIncrZoom, minZoomVect.y + maxIncrZoom, minZoomVect.z + maxIncrZoom);
        zoomSpeedVect = new Vector3(zoomSpeed, zoomSpeed, zoomSpeed);

        maxZoomScale = minZoomVect.x + maxIncrZoom;
        
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
        }
        else{
            newZoomVect = mouseScroll*zoomSpeedVect + visionCanvas.transform.localScale;
            // playerCanvas.transform.localScale -= mouseScroll*zoomSpeedVect;
        }

        visionCanvas.transform.localScale = newZoomVect;  
    }
}
