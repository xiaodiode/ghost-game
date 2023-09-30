using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekinesisGhost : Ghost
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator startTamperings(){
        // while(tamperingList.Count != 0){
        //     randomIndex = Mathf.FloorToInt(Random.Range(0, tamperingList.Count - 1));
        //     tamperingList[randomIndex]();
        //     tamperingList.RemoveAt(randomIndex);

            yield return new WaitForSeconds(triggerInterval);
        // }
    
    }
}
