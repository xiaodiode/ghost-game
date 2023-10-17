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
    [SerializeField] private float maxZoom;
    [SerializeField] private float minZoom;
    [SerializeField] private float zoomSpeed;

    float mouseScroll;
    float newZoom;

    Vector2 movePos;
    public void Start()
    {
        // RectTransformUtility.ScreenPointToLocalPointInRectangle(
        //     parentCanvas.transform as RectTransform, Input.mousePosition,
        //     parentCanvas.worldCamera,
        //     out movePos);

        minZoom = zoomCamera.fieldOfView;
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
        newZoom = zoomCamera.fieldOfView - mouseScroll*zoomSpeed;
        newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);

        zoomCamera.fieldOfView = newZoom;  
    }
}
