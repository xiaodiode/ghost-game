using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;

public class TextInputController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private PlayerController player;

    [SerializeField] private DataLoader dataLoader;
    [SerializeField] private TextMeshProUGUI autocomplete;
    
    
    [SerializeField] private string firstDemonMatch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData){
        player.lockMovement = true;
        player.sceneController.lockMovement = true;
    }

    public void triggerAutocomplete(string input){
        if(input != ""){
            firstDemonMatch = dataLoader.demonList.FirstOrDefault(demon => demon.StartsWith(input, System.StringComparison.OrdinalIgnoreCase));
            Debug.Log("demonMatch: " + firstDemonMatch);
        }
        else{
            firstDemonMatch = "";
        }

        autocomplete.text = firstDemonMatch;
    }

    public void clearAutocomplete(string input){
        Debug.Log("clearing");
        autocomplete.text = "";
        inputField.text = "";

        player.lockMovement = false;
        player.sceneController.lockMovement = false;
    }
}
