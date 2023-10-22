using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lightable : MonoBehaviour
{   
    [SerializeField] public bool isLit;

    [Header("Light Tampering")]
    [SerializeField] private bool litOverlay;
    [SerializeField] private bool changeSprite;

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
    [SerializeField] private int flickerOnCount;
    [SerializeField] private float flickerOnInterval;
    [SerializeField] private float flickerPercentFall;
    
    [Header("Fade In Settings")]
    [SerializeField] private float maxFadeIntensity;
    [SerializeField] private float fadeDuration;

    [Header("Continuous Lit Animation Options")]
    [SerializeField] private bool flicker;
    [SerializeField] private bool glow;

    [Header("Flicker Settings")]
    [SerializeField] private int flickerCount;
    [SerializeField] private float flickerInterval;
    [SerializeField] private float darkDuration;

    [Header("Glow Settings")]
    [SerializeField] private float maxFallStrength;
    [SerializeField] private float minFallStrength;
    [SerializeField] private float glowDuration;

    float elapsedTime;
    bool switchAnimReady;
    
    // Start is called before the first frame update
    void Start()
    {
        switchAnimReady = true;

        if(isLit){
            light2D.enabled = true;
        }
        else{
            light2D.enabled = false;
        }

        idealIntensity = light2D.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("switchanimeready: " + switchAnimReady);
    }

    public void onInteraction(){
        if(switchAnimReady){
            switchAnimReady = false;
            StartCoroutine(changeLitState());
        }
    }

    public IEnumerator changeLitState(){
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
        if(flickerOn){
            int counter = flickerOnCount;
            
            while(counter != 0){
                light2D.enabled = !lightOn;

                yield return new WaitForSeconds(flickerOnInterval*(counter*flickerPercentFall));

                light2D.enabled = lightOn;

                yield return new WaitForSeconds(flickerOnInterval*(counter*flickerPercentFall));

                counter--;
            }
            switchAnimReady = true;
        }

        else if(fadeIn){
            elapsedTime = 0;
            
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
            switchAnimReady = true;
        }

        else if(instant){
            light2D.enabled = lightOn;
            switchAnimReady = true;
        }
        

    }

    private IEnumerator litAnimation(){
        while(isLit){
            if(flicker){
                int counter = flickerCount;
                while(counter != 0){
                    light2D.enabled = !light2D.enabled;

                    yield return new WaitForSeconds(counter*darkDuration*flickerPercentFall);

                    light2D.enabled = !light2D.enabled;

                    yield return new WaitForSeconds(counter*darkDuration*flickerPercentFall);

                    counter--;
                }

                yield return new WaitForSeconds(flickerInterval);
            }
            if(glow){
                elapsedTime = 0;

                while(elapsedTime < glowDuration){
                    if(elapsedTime < glowDuration/2){
                        light2D.falloffIntensity = Mathf.Lerp(maxFallStrength, minFallStrength, elapsedTime/(glowDuration/2));
                    }
                    else{
                        light2D.falloffIntensity = Mathf.Lerp(minFallStrength, maxFallStrength, (elapsedTime-glowDuration/2)/(glowDuration/2));
                    }
                    
                    elapsedTime += Time.deltaTime;

                    yield return null; 
                }
            }
            yield return null;
        }
    }


}
