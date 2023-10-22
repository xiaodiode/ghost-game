using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] public bool isBreaking;
    [SerializeField] private bool isBroken;

    [Header("Breakable Object Properties")]
    [SerializeField] private SpriteRenderer unbrokenState;
    [SerializeField] private SpriteRenderer brokenState;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private float heaviness;
    [SerializeField] private bool onWall;
    [SerializeField] private bool onTable;
    [SerializeField] private bool isStandalone;

    [Header("Breakable Sprite State Settings")]
    [SerializeField] private bool changeSprite;
    [SerializeField] private bool brokenOverlay;

    [Header("Wall Object Animation Settings")]
    [SerializeField] private bool canSwing;
    [SerializeField] private AudioClip swingingSound;
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
    [SerializeField] private AudioClip vibrateSound;
    [SerializeField] private float baseMaxVibrateSpeed;
    [SerializeField] private float actualMaxVibrateSpeed;
    [SerializeField] private float baseVibrateDistance;
    [SerializeField] private float actualVibrateDistance;
    [SerializeField] private float vibrateDuration;

    [Header("Teeter Animation Settings")]
    [SerializeField] private bool canTeeter;
    [SerializeField] private AudioClip teeterSound;
    [SerializeField] private float maxTeeterAngle;
    [SerializeField] private float teeterSpeed;
    [SerializeField] private float teeterInDuration;

    [Header("Fall Animation Settings")]
    [SerializeField] private bool canFall;
    [SerializeField] private bool fallClockwise;
    [SerializeField] private bool madeImpact;
    [SerializeField] private float impactAngle;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float XFallDistance;
    [SerializeField] private float YFallDistance;

    [Header("Bounce Animation Settings")]
    [SerializeField] private bool canBounce;
    [SerializeField] private AudioClip bounceSound;
    [SerializeField] private float initialYBounce;
    [SerializeField] [Range(0,1)] private float heightPercentDrop;
    

    bool initialMovementReady = false, touchedGround = false, finishedBouncing = false,
        isFalling = false;


    // Start is called before the first frame update
    void Start()
    {
        isBroken = false;
        isBreaking = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void breakObject(){

        if(!isBreaking){
            isBreaking = true;
            StartCoroutine(breakProcedure());
        }
    }

    private IEnumerator breakProcedure(){
        
        initialMovement();

        while(!initialMovementReady){
            yield return null;
        }

        if(canFall){
           StartCoroutine(startFalling()); 
        }
        else{
            touchedGround = true;
        }

        while(!touchedGround){
            yield return null;
        }

        if(canBounce){
            StartCoroutine(startBouncing());
        }
        else{
            finishedBouncing = true;
        }

        while(!finishedBouncing){
            yield return null;
        }

        changeToBroken();
        
    }

    private void initialMovement(){

        if(onWall){
            StartCoroutine(startSwinging());
        }
        else if(onTable){
            StartCoroutine(startShifting());
        }
        else if(isStandalone){
            StartCoroutine(startToppling());
        }

    }

    private IEnumerator startSwinging(){
        if(canVibrate){

        }
        yield return null;
    }

    private IEnumerator startShifting(){
        
        yield return null;
    }

    private IEnumerator startToppling(){
        
        yield return null;
    }

    private IEnumerator startVibrating(){

        yield return null;
    }

    private IEnumerator startFalling(){

        yield return null;
    }

    private IEnumerator startBouncing(){

        yield return null;
    }

    private void changeToBroken(){
        if(brokenOverlay){
            brokenState.enabled = true;
        }
        else if(changeSprite){
            unbrokenState.enabled = false;
            brokenState.enabled = true;
        }

        isBroken = true;
    }



}
