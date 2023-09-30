using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakingGhost : Ghost
{
    [SerializeField] private List<GameObject> perfectObjects = new();
    [SerializeField] private List<GameObject> brokenObjects = new();
    [SerializeField] private delegate void triggerings(GameObject a, GameObject b);
    [SerializeField] private List<triggerings> tamperingList = new List<triggerings>();

    private triggerings trigger = breakObject;
    // Start is called before the first frame update
    void Start()
    {
        type = "unsubtle";

        trigger(perfectObjects[0],brokenObjects[0]);
        for(int i=0; i<perfectObjects.Count; i++){
            tamperingList.Add(trigger);   
            // tamperingList.Add(please()); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static void breakObject(GameObject goodObject, GameObject badObject){
        Vector3 currPosition = goodObject.transform.position;
        Quaternion currRotation = goodObject.transform.rotation;

        Instantiate<GameObject>(badObject, currPosition, currRotation);

        badObject.transform.localScale = goodObject.transform.localScale;

        Destroy(goodObject);
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
