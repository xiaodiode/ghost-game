using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionController : MonoBehaviour
{
    [SerializeField] private Canvas parentCanvas;
    [SerializeField] private MouseController mouse;

    Vector2 movePos;
    public void Start()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parentCanvas.transform as RectTransform, Input.mousePosition,
            parentCanvas.worldCamera,
            out movePos);
    }

    public void Update()
    {
        if(!mouse.isLocked){
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);

            transform.position = parentCanvas.transform.TransformPoint(movePos);
        }
        
    }
}
