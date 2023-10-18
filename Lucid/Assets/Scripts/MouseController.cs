using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    [SerializeField] public bool isLocked;

    Vector3 oldMousePosition;
    // Start is called before the first frame update
    void Start()
    {
        isLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocked){
            Mouse.current.WarpCursorPosition(oldMousePosition);
        }
    }

    public void lockMouse(bool locked){
        if(locked){
            oldMousePosition = Input.mousePosition;
            Cursor.visible = false;
            isLocked = true;
        }
        else{
            Mouse.current.WarpCursorPosition(oldMousePosition);
            Cursor.visible = true;
            isLocked = false;
        }
    }
}
