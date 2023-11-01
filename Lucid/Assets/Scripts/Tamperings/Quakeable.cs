using System.Collections;
using UnityEngine;

public class Quakeable : MonoBehaviour
{
    // [SerializeField] public bool isQuaking;
    // [SerializeField] public bool hasQuaked;

    [Header("Controllers")]
    [SerializeField] private SceneController sceneController;
    [SerializeField] private QuakeableController quakeableController;


    [Header("Quakeable Object Properties")]
    [SerializeField] private RectTransform objectRect;
    [SerializeField] private SpriteRenderer unquakedState;
    [SerializeField] private SpriteRenderer quakedState;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private float weightMultiplier;
    [SerializeField] private bool onWall;
    [SerializeField] private bool onTable;
    [SerializeField] private bool isStandalone;


    [Header("Quakeable Sprite State Settings")]
    [SerializeField] private bool changeSprite;
    [SerializeField] private bool quakedOverlay;


    [Header("Wall Object Animation Settings")]
    [SerializeField] private AudioClip swingingSound;
    [SerializeField] [Range(0.5f, 2)] private float minSwingAngle;
    [SerializeField] [Range(10f, 20f)] private float maxSwingAngle;
    [SerializeField] [Range(2f, 6f)] private float swingInterval;
    [SerializeField] private float swingDuration; 


    [Header("Table Object Animation Settings")]
    [SerializeField] private bool canShift;
    [SerializeField] private bool shiftRight;
    [SerializeField] [Range(0, 1f)] private float shiftDelay;
    [SerializeField] [Range(0.5f, 10f)] private float XDistanceToFall;


    [Header("Vibrate Animation Settings")]
    [SerializeField] private bool canVibrate;
    [SerializeField] private AudioClip vibrateSound;
    [SerializeField] [Range(0, 1f)] private float vibrateDelay;
    [SerializeField] [Range(0, 0.03f)] private float vibrateIntervalFactor;
    [SerializeField] [Range(0, 0.1f)]private float vibratePercentFall;
    [SerializeField] [Range(0, 0.05f)] private float vibrateXDistance;
    [SerializeField] private float vibrateDuration;
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
    
    Vector2 initialPartPos; 
    Vector3 initialPartRot;
    Vector2 shiftDistance;

    bool touchedGround = false, finishedBouncing = false,
        isVibrating = false, isTeetering = false, isShifting = false, isSwinging = false,
        isToppling = false, isFalling = false, isBouncing = false;


    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void quakeObject(){

        initialPartPos = objectRect.anchoredPosition;
        initialPartRot = transform.rotation.eulerAngles;

        vibrateInterval = vibrateDuration*vibrateIntervalFactor;


        // if(!isQuaking && !hasQuaked){
        //     isQuaking = true;
        StartCoroutine(quakeProcedure());

        Debug.Log("starting quaking procedure");
        // }
    }

    private IEnumerator quakeProcedure(){
        
        initialMovement();

        while(!isVibrating && !isSwinging && !isShifting && !isTeetering && !isToppling){
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

        // isQuaking = false;

        changeToQuaked();
        
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
            if(canShift){
                isShifting = true;
                if(!shiftRight){
                    XDistanceToFall = -XDistanceToFall;
                }
                float shiftXDistance = XDistanceToFall/((vibrateDuration - shiftDelay)*(40/vibrateDuration + 3)); //40 = 4, 20 = 5, 10 = 8, 5 = 13: 10, 4, 1.25
                shiftDistance = new Vector2(shiftXDistance, 0);
            }
            if(canTeeter){
                isTeetering = true;
                StartCoroutine(startTeetering());
            }
        }
        else if(isStandalone){
            isToppling = true;
            StartCoroutine(startToppling());
        }

    }

    private IEnumerator startVibrating(){
        float elapsedTime = 0;
        float elapsedInterval = 0;
        
        Vector2 vibrateRefPos = initialPartPos;
        Vector2 XTranslation = new Vector2(vibrateXDistance, 0);
        
        yield return new WaitForSeconds(vibrateDelay);

        while(elapsedTime < vibrateDuration){

            if(sceneController.isMoving){
                yield return null;
            }

            else{
               if(elapsedInterval > vibrateInterval){
                    elapsedInterval = 0;    
                    vibrateInterval *= 1-(vibrateInterval*vibratePercentFall);

                    if(canShift && elapsedTime >= shiftDelay){
                        vibrateRefPos += shiftDistance;
                    }
                    
                }
                
                if(elapsedInterval < (vibrateInterval/2)){
                    objectRect.anchoredPosition = Vector2.Lerp(vibrateRefPos, vibrateRefPos + XTranslation, elapsedInterval/(vibrateInterval/2));
                }
                else{
                    objectRect.anchoredPosition = Vector2.Lerp(vibrateRefPos + XTranslation, vibrateRefPos, (elapsedInterval - vibrateInterval/2)/(vibrateInterval/2));
                }

                elapsedInterval += Time.deltaTime;
                elapsedTime += Time.deltaTime;

                yield return null; 
            }
            
        }

        isVibrating = false;

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

        isTeetering = false;
    }

    private IEnumerator startSwinging(){
        isSwinging = true;

        float elapsedTime = 0;
        float elapsedInterval = 0;
        float currPeakAngle = minSwingAngle;
        float currentInterval = swingInterval;
        float velocity;
        float currentAngle = objectRect.rotation.eulerAngles.z;

        Quaternion newRotation = Quaternion.Euler(new Vector3());
        
        while(elapsedTime < swingDuration){
            if(elapsedInterval > currentInterval){
                elapsedInterval = 0;    
            
                if(elapsedTime < (swingDuration/2)){
                    currPeakAngle = Mathf.SmoothStep(minSwingAngle, maxSwingAngle, elapsedTime/(swingDuration/2));
                }
                else{
                    currPeakAngle = Mathf.SmoothStep(maxSwingAngle, minSwingAngle, (elapsedInterval - swingDuration/2)/(swingDuration/2));
                }

                initialPartRot = objectRect.rotation.eulerAngles;
                
            }
            else{
                if(elapsedInterval < currentInterval/2){
                    if(elapsedInterval < currentInterval/4){
                        velocity = Mathf.Lerp(0, currPeakAngle, elapsedInterval/(currentInterval/4));
                    }
                    else{
                        velocity = Mathf.Lerp(currPeakAngle, 0, elapsedInterval/(currentInterval/2));
                    }
                    currentAngle += velocity * Time.deltaTime;
                    
                }
                else if(elapsedInterval < currentInterval){
                    if(elapsedInterval < 3*currentInterval/4){
                        velocity = Mathf.Lerp(0, currPeakAngle, elapsedInterval/(3*currentInterval/4));
                    }
                    else{
                        velocity = Mathf.Lerp(currPeakAngle, 0, elapsedInterval/currentInterval);
                    }
                    currentAngle -= velocity * Time.deltaTime;
                    
                }
            }
            

            newRotation.eulerAngles = new Vector3(0, 0, currentAngle);

            objectRect.rotation = newRotation;

            elapsedInterval += Time.deltaTime;
            elapsedTime += Time.deltaTime;

            

            yield return null;
        }
        // if (isSwinging)
        // {
        //     // Calculate the target angular velocity based on the swingSpeed
        //     float targetVelocity = Mathf.Sign(swingAngle - currentAngle) * swingSpeed;

        //     // Smoothly dampen the velocity using Mathf.SmoothDamp
        //     velocity = Mathf.SmoothDamp(velocity, targetVelocity, ref velocity, swingDelay);

        //     // Update the current angle based on the angular velocity
        //     currentAngle += velocity * Time.deltaTime;

        //     // Clamp the angle within the range
        //     currentAngle = Mathf.Clamp(currentAngle, -swingAngle, swingAngle);

        //     // Rotate the object
        //     transform.rotation = Quaternion.Euler(0, currentAngle, 0);
        // }

        // // Check if the object has reached the end of the swing
        // if (Mathf.Approximately(currentAngle, -swingAngle) || Mathf.Approximately(currentAngle, swingAngle))
        // {
        //     // Object has reached the end of the swing, hold for a bit
        //     isSwinging = false;
        //     Invoke("StartSwing", swingDelay);
        // }



        // while(elapsedTime < swingDuration){
        //     if(elapsedInterval > currentInterval){

                
        //         elapsedInterval = 0;    

        //         if(elapsedTime < (swingDuration/2)){
        //             currPeakAngle = Mathf.SmoothStep(minSwingAngle, maxSwingAngle, elapsedTime/(swingDuration/2));
        //         }
        //         else{
        //             currPeakAngle = Mathf.SmoothStep(maxSwingAngle, minSwingAngle, (elapsedInterval - currentInterval/2)/(currentInterval/2));
        //         }

        //         targetRightRotation = new Vector3(0, 0, -currPeakAngle);
        //         targetLeftRotation = new Vector3(0, 0, currPeakAngle);

        //         initialPartRot = objectRect.rotation.eulerAngles;
                
        //     }

        //     else if(elapsedInterval < (currentInterval/2)){
        //         newRotation.eulerAngles = Vector3.Lerp(initialPartRot, targetRightRotation, elapsedInterval/(currentInterval/2));
        //         // newRotation.eulerAngles = Vector3.Lerp(initialPartRot, targetRightRotation, 1 - Mathf.Exp(currentInterval/2 - Time.deltaTime ));
                
        //     }
        //     else{
        //         newRotation.eulerAngles = Vector3.Lerp(targetRightRotation, targetLeftRotation, (elapsedInterval - currentInterval/2)/(currentInterval/2));
        //         // newRotation.eulerAngles = Vector3.Lerp(targetRightRotation, targetLeftRotation, 1 - Mathf.Exp(currentInterval/2 - Time.deltaTime ));
                
        //     }

        //     objectRect.rotation = newRotation;

        //     elapsedInterval += Time.deltaTime;
        //     elapsedTime += Time.deltaTime;

        //     yield return null;
        // }

        isSwinging = false;
    }

    private IEnumerator startToppling(){
        
        yield return null;
        isToppling = false;
    }

    private IEnumerator startFalling(){

        yield return null;
        isFalling = false;
    }

    private IEnumerator startBouncing(){

        yield return null;
        isBouncing = false;
    }

    private void changeToQuaked(){
        if(quakedOverlay){
            quakedState.enabled = true;
        }
        else if(changeSprite){
            unquakedState.enabled = false;
            quakedState.enabled = true;
        }

        // hasQuaked = true;
        quakeableController.updateQuakingStatus();
    }



}
