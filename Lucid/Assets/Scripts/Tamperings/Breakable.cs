using System.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] public bool isBreaking;
    [SerializeField] private bool isBroken;


    [Header("Breakable Object Properties")]
    [SerializeField] private SpriteRenderer unbrokenState;
    [SerializeField] private SpriteRenderer brokenState;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private float weightMultiplier;
    [SerializeField] private bool onWall;
    [SerializeField] private bool onTable;
    [SerializeField] private bool isStandalone;


    [Header("Breakable Sprite State Settings")]
    [SerializeField] private bool changeSprite;
    [SerializeField] private bool brokenOverlay;


    [Header("Wall Object Animation Settings")]
    [SerializeField] private bool canSwing;
    [SerializeField] private AudioClip swingingSound;
    [SerializeField] private float maxSwingAngle; // = 40;
    [SerializeField] private float startSwingAngle; // = -40;
    [SerializeField] private float peakAngleWaittime;
    [SerializeField] private float peakAngleOffset;
    [SerializeField] private float swingAngleMultiplier;
    [SerializeField] private int swingCount;
    [SerializeField] private float baseSwingDuration; 
    private float actualMaxSwingAngle, actualStartSwingAngle, actualSwingDuration,
        actualPeakAngleWait, actualPeakAngleOffset;


    [Header("Surface Object Animation Settings")]
    [SerializeField] private float moveRight;
    [SerializeField] private float moveDuration;
    [SerializeField] private float XDistanceToFall;
    
    [Header("Vibration Animation Settings")]
    [SerializeField] private bool canVibrate;
    [SerializeField] private AudioClip vibrateSound;
    [SerializeField] private float vibrateInterval;
    [SerializeField] private float vibratePercentFall;
    [SerializeField] private float vibrateXDistance;
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
    
    Vector3 initialPosition;
    bool initialMovementReady = false, touchedGround = false, finishedBouncing = false,
        isFalling = false, isVibrating = false;


    // Start is called before the first frame update
    void Start()
    {
        isBroken = false;
        isBreaking = false;

        initialPosition = transform.position;

        initializeActuals();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initializeActuals(){
        if(canSwing){
            actualMaxSwingAngle = maxSwingAngle/weightMultiplier;
            actualStartSwingAngle = startSwingAngle/weightMultiplier;
            actualSwingDuration = baseSwingDuration*weightMultiplier; 
        }
        
    }

    public void breakObject(){

        if(!isBreaking && !isBroken){
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

        isBreaking = false;
        
        changeToBroken();
        
    }

    private void initialMovement(){

        if(canVibrate){
            isVibrating = true;
            StartCoroutine(startVibrating());
        }


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

    private IEnumerator startVibrating(){
        float elapsedTime = 0;
        float elapsedInterval;

        Vector3 XTranslation = transform.position + new Vector3(vibrateXDistance, 0, 0);

        while(elapsedTime < vibrateDuration){
            elapsedInterval = 0;

            if(elapsedInterval < (vibrateInterval/2)){
                transform.position = Vector3.Lerp(initialPosition, XTranslation, elapsedInterval/(vibrateInterval/2));
            }
            else{
                transform.position = Vector3.Lerp(XTranslation, initialPosition, (elapsedInterval - vibrateInterval/2)/(vibrateInterval/2));
            }

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return null;
    }

    private IEnumerator startSwinging(){
        while(!isVibrating){
            yield return null;
        }

        if(canSwing){

        }

        
    }

    private IEnumerator startShifting(){
        
        yield return null;
    }

    private IEnumerator startToppling(){
        
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
