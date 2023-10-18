using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lightable : MonoBehaviour
{   
    [SerializeField] public bool isLit;

    [Header("Light Tampering")]
    [SerializeField] private bool litOverlay, changeSprite;

    [Header("Lightable Object Properties")]
    [SerializeField] private SpriteRenderer unlitState;
    [SerializeField] private SpriteRenderer litState;
    [SerializeField] private Light2D light2D;
    [SerializeField] private float idealIntensity;
    
    [Header("Switch On/Off Animations Options")]
    [SerializeField] private bool flickerOn;
    [SerializeField] private bool fadeIn;
    [SerializeField] private bool instant;

    [Header("Flicker On Settings")]
    [SerializeField] private int flickerCount;
    [SerializeField] private float flickerOnInterval;
    [SerializeField] private float flickerPercentFall;
    
    [Header("Fade In Settings")]
    [SerializeField] private float maxFadeIntensity;
    [SerializeField] private float fadeDuration;

    [Header("Continuous Lit Animation Options")]
    [SerializeField] private bool flicker;
    [SerializeField] private bool glow;

    [Header("Flicker Settings")]
    [SerializeField] private float flickerInterval;
    [SerializeField] private float darkDuration;

    [Header("Glow Settings")]
    [SerializeField] private float maxFallStrength;
    [SerializeField] private float minFallStrength;
    [SerializeField] private float glowDuration;

    int counter;
    float elapsedTime;
    bool switchAnimReady;
    
    // Start is called before the first frame update
    void Start()
    {
        idealIntensity = light2D.intensity;

        StartCoroutine(changeLitState());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onInteraction(){
        StartCoroutine(changeLitState());
    }

    public IEnumerator changeLitState(){
        switchAnimReady = false;
        if(litOverlay){
            litState.enabled = !isLit;
        }
        else if(changeSprite){
            unlitState.enabled = isLit;
            litState.enabled = !isLit;
        }

        StartCoroutine(switchAnimation(!isLit));

        while(!switchAnimReady){
            yield return null;
        }

        isLit = !isLit;

        if(isLit){
            StartCoroutine(litAnimation());
        }
        

    }

    private IEnumerator switchAnimation(bool lightOn){
        if(instant){
            light2D.enabled = lightOn;
        }
        else if(flickerOn){
            Debug.Log("in flicker on");
            counter = flickerCount;
            
            while(counter != 0){
                Debug.Log("counter: " + counter);
                light2D.enabled = !lightOn;

                Debug.Log("flickerOnInterval*(counter*flickerPercentFall: " + flickerOnInterval*(counter*flickerPercentFall));
                yield return new WaitForSeconds(flickerOnInterval*(counter*flickerPercentFall));

                light2D.enabled = lightOn;

                yield return new WaitForSeconds(flickerOnInterval*(counter*flickerPercentFall));

                counter--;
            }
        }
        else if(fadeIn){
            while(elapsedTime < fadeDuration){
                if(lightOn){
                    light2D.intensity = Mathf.Lerp(0, maxFadeIntensity, elapsedTime/fadeDuration);
                }
                else{
                    light2D.intensity = Mathf.Lerp(maxFadeIntensity, 0, elapsedTime/fadeDuration);
                }
                
                elapsedTime += Time.deltaTime;

                yield return null; 
            }
        }

        switchAnimReady = true;

    }

    private IEnumerator litAnimation(){
        yield return null;
    }
}
