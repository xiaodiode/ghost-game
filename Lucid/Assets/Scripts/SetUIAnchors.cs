using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUIAnchors : MonoBehaviour
{
    public Canvas canvas;
    public RectTransform uiElement;
    public Transform targetObject;
    
    private RectTransform parentRectTransform;

    private void Start()
    {
        // Get the parent RectTransform of the UI element
        parentRectTransform = uiElement.transform.parent as RectTransform;
    }

    private void Update()
    {
        // Convert the target object's world position to screen coordinates
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetObject.position);

        // Convert the screen coordinates to local coordinates of the parent RectTransform
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, screenPosition, canvas.worldCamera, out localPosition);

        // Set the UI element's anchored position to the converted local coordinates
        uiElement.anchoredPosition = localPosition;
    }
}
