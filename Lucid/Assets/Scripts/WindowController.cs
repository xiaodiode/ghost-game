using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WindowController : MonoBehaviour
{
    [SerializeField] private GameObject gradientPivot;
    [SerializeField] private ClockController accurateClock;
    [SerializeField] private Light2D windowLight;
    [SerializeField] private float maxLightIntensity;

    Vector3 currGradientRotation;
    float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        // 2 full cycles of hour hand = 1 full cycle of gradient circle
        rotationSpeed = accurateClock.rotationSpeed/2;  
    }

    // Update is called once per frame
    void Update()
    {
        updateGradientRotation();
    }

    private void updateGradientRotation(){
        currGradientRotation = gradientPivot.transform.rotation.eulerAngles;
        currGradientRotation.z += rotationSpeed*Time.deltaTime;
        updateWindowLight(currGradientRotation.z);
        
        gradientPivot.transform.rotation = Quaternion.Euler(currGradientRotation);
    }

    private void updateWindowLight(float rotation){
        if(rotation <= 180){
            windowLight.intensity = (rotation/180)*maxLightIntensity;
        }
        else{
            windowLight.intensity = maxLightIntensity - ((rotation-180)/180)*maxLightIntensity;
        }
    }
}
