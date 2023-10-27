using System.Collections;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] public bool isBreaking;
    [SerializeField] public bool isBroken;


    [Header("Breakable Object Properties")]
    [SerializeField] private GameObject breakableBody;
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

    
    [Header("Vibrate/Shift Animation Settings")]
    [SerializeField] private bool canVibrate;
    [SerializeField] private AudioClip vibrateSound;
    [SerializeField] [Range(0, 1f)] private float vibrateDelay;
    [SerializeField] [Range(0, 0.03f)] private float vibrateIntervalFactor;
    [SerializeField] [Range(0, 0.1f)]private float vibratePercentFall;
    [SerializeField] [Range(0, 0.05f)] private float vibrateXDistance;
    [SerializeField] private float vibrateDuration;
    [SerializeField] private bool canShift;
    [SerializeField] private bool shiftRight;
    [SerializeField] [Range(0, 1f)] private float shiftDelay;
    [SerializeField] [Range(0.5f, 10f)] private float XDistanceToFall;
    private float vibrateInterval;


    [Header("Teeter Animation Settings")]
    [SerializeField] private bool canTeeter;
    [SerializeField] private AudioClip teeterSound;
    [SerializeField] [Range(0, 1f)] private float teeterDelay;
    [SerializeField] [Range(0.5f, 1.5f)] private float maxTeeterInterval;
    [SerializeField] [Range(0f, 0.5f)] private float minTeeterInterval;
    [SerializeField] [Range(0.2f, 0.5f)] private float teeterPercentFall;
    [SerializeField] [Range(0, 20f)] private float maxTeeterAngle;
    [SerializeField] [Range(0f, 5f)] private float minTeeterAngle;


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
    
    Vector3 initialPartPos, initialPartRot, initialBodyPos, initialBodyRot;

    bool initialMovementReady = false, touchedGround = false, finishedBouncing = false,
        isFalling = false, isVibrating = false;


    // Start is called before the first frame update
    void Start()
    {
        isBroken = false;
        isBreaking = false;

        initialPartPos = transform.position;
        initialPartRot = transform.rotation.eulerAngles;

        vibrateInterval = vibrateDuration*vibrateIntervalFactor;

        // initialBodyPos = breakableBody.transform.position;
        // initialBodyRot = breakableBody.transform.rotation.eulerAngles;

        initializeActuals();

        breakObject();

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

            Debug.Log("starting breaking procedure");
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
        if(canTeeter){
            StartCoroutine(startTeetering());
        }



        if(onWall){
            StartCoroutine(startSwinging());
        }
        else if(onTable){
            
        }
        else if(isStandalone){
            StartCoroutine(startToppling());
        }

    }

    private IEnumerator startVibrating(){
        float elapsedTime = 0;
        float elapsedInterval = 0;

        Vector3 shiftDistance = new();

        if(canShift){
            if(!shiftRight){
                XDistanceToFall = -XDistanceToFall;
            }
            float shiftXDistance = XDistanceToFall/((vibrateDuration - shiftDelay)*(40/vibrateDuration + 3)); //40 = 4, 20 = 5, 10 = 8, 5 = 13: 10, 4, 1.25
            shiftDistance = new Vector3(shiftXDistance, 0, 0);
        }
        
        Vector3 vibrateRefPos = initialPartPos;
        Vector3 XTranslation = new Vector3(vibrateXDistance, 0, 0);
        
        yield return new WaitForSeconds(vibrateDelay);

        while(elapsedTime < vibrateDuration){
            Debug.Log("vibrateRefPos: " + vibrateRefPos + " vibrateRefPos + XTranslation: " + vibrateRefPos + XTranslation);
            if(elapsedInterval > vibrateInterval){
                elapsedInterval = 0;    
                vibrateInterval *= 1-(vibrateInterval*vibratePercentFall);

                if(canShift && elapsedTime >= shiftDelay){
                    vibrateRefPos += shiftDistance;
                }
                
            }
            
            if(elapsedInterval < (vibrateInterval/2)){
                transform.position = Vector3.Lerp(vibrateRefPos, vibrateRefPos + XTranslation, elapsedInterval/(vibrateInterval/2));
            }
            else{
                transform.position = Vector3.Lerp(vibrateRefPos + XTranslation, vibrateRefPos, (elapsedInterval - vibrateInterval/2)/(vibrateInterval/2));
            }

            elapsedInterval += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            yield return null;
        }

    }

    private IEnumerator startTeetering(){
        yield return new WaitForSeconds(vibrateDelay + teeterDelay);

        float elapsedTime = 0;
        float elapsedInterval = 0;
        float currentAngle = minTeeterAngle;
        float currentInterval = maxTeeterInterval;

        Quaternion newRotation = Quaternion.Euler(new Vector3());
        Vector3 targetRightRotation = new Vector3(0, 0, -currentAngle);
        Vector3 targetLeftRotation = new Vector3(0, 0, currentAngle);

        while(elapsedTime < vibrateDuration + teeterDelay){
            if(elapsedInterval > currentInterval){
                elapsedInterval = 0;    

                currentInterval *= 1 - (currentInterval*teeterPercentFall);
                currentInterval = Mathf.Clamp(currentInterval, minTeeterInterval, maxTeeterInterval);

                currentAngle *= 1 - teeterPercentFall;
                currentAngle = Mathf.Clamp(currentAngle, minTeeterAngle, maxTeeterAngle);

                targetRightRotation = new Vector3(0, 0, -currentAngle);
                targetLeftRotation = new Vector3(0, 0, currentAngle);

                initialPartRot = transform.rotation.eulerAngles;
            }
            
            if(elapsedInterval < (currentInterval/2)){
                newRotation.eulerAngles = Vector3.LerpUnclamped(initialPartRot, targetRightRotation, elapsedInterval/(currentInterval/2));
            }
            else{
                newRotation.eulerAngles = Vector3.LerpUnclamped(targetRightRotation, targetLeftRotation, (elapsedInterval - currentInterval/2)/(currentInterval/2));
            }

            transform.rotation = newRotation;

            elapsedInterval += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    private IEnumerator startSwinging(){
        while(!isVibrating){
            yield return null;
        }

        if(canSwing){

        }

        
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
