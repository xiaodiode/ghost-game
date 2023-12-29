using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Instruments : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip instrumentSound;
    [SerializeField] private AudioClip trumpetSound;
    [SerializeField] private AudioClip currSound;

    // Start is called before the first frame update
    void Start()
    {
        currSound = instrumentSound;

        source.clip = currSound;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(){
        source.Play();
    }

    public void changeToTrumpet(){
        currSound = trumpetSound;

        source.clip = currSound;
    }
}
