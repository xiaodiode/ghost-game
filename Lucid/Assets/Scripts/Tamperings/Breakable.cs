using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] public bool isBroken;

    [Header("Breakable Object Properties")]
    [SerializeField] private SpriteRenderer unbrokenState;
    [SerializeField] private SpriteRenderer brokenState;
    [SerializeField] private GameObject[] children;
    [SerializeField] private float objectWeight;
    [SerializeField] private bool onWall;
    [SerializeField] private bool onTable;

    [Header("Breakable Sprite State Settings")]
    [SerializeField] private bool changeSprite;
    [SerializeField] private bool brokenOverlay;

    [Header("Wall Object Animation Settings")]
    [SerializeField] private bool canSwing;
    [SerializeField] private float baseMaxSwingAngle; // = 40;
    [SerializeField] private float baseMinSwingAngle; // = -40;
    [SerializeField] private float actualMaxSwingAngle;
    [SerializeField] private float actualMinSwingAngle;
    [SerializeField] private int swingCount;
    [SerializeField] private float swingDuration; 

    [Header("Surface Object Animation Settings")]
    [SerializeField] private float moveRight;
    [SerializeField] private float moveDuration;
    [SerializeField] private float XDistanceToFall;
    
    [Header("Vibration Animation Settings")]
    [SerializeField] private bool canVibrate;
    [SerializeField] private float baseMaxVibrateSpeed;
    [SerializeField] private float actualMaxVibrateSpeed;
    [SerializeField] private float baseVibrateDistance;
    [SerializeField] private float actualVibrateDistance;
    [SerializeField] private float vibrateDuration;

    [Header("Teeter Animation Settings")]
    [SerializeField] private bool canTeeter;
    [SerializeField] private float maxTeeterAngle;
    [SerializeField] private float teeterSpeed;
    [SerializeField] private float teeterInDuration;

    [Header("Fall Animation Settings")]
    [SerializeField] private bool canFall;
    [SerializeField] private bool fallClockwise;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float YFallDistance;

    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
