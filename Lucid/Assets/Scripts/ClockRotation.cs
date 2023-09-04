using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockRotation : MonoBehaviour
{
    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currRotation = transform.rotation.eulerAngles;
        currRotation.z -= rotationSpeed*Time.deltaTime;
        
        transform.rotation = Quaternion.Euler(currRotation);
    }
}
