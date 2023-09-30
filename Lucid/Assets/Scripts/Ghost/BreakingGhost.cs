using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakingGhost : Ghost
{
    [SerializeField] private List<GameObject> perfectObjects = new();
    [SerializeField] private List<GameObject> brokenObjects = new();
    [SerializeField] private delegate void triggerings(GameObject a, GameObject b);

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i<perfectObjects.Count; i++){
            // tamperingList.Add(breakObject(perfectObjects[i],brokenObjects[i]));   
            // tamperingList.Add(please()); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void please(){

    }
    private void breakObject(GameObject goodObject, GameObject badObject){
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
