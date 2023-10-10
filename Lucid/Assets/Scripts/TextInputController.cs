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

    [SerializeField] private DataLoader data;
    [SerializeField] private TextMeshProUGUI autocomplete;
    
    
    [SerializeField] private string firstDemonMatch;

    string latestGuess;
    bool validDemon;
    // Start is called before the first frame update
    void Start()
    {
        validDemon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inputField.isFocused && Input.GetKeyDown(KeyCode.Tab) && firstDemonMatch != ""){
            inputField.text = firstDemonMatch;
        }

        if(!inputField.isFocused && Input.GetKeyDown(KeyCode.Return)){
            enterGuess();
        }
    }

    public void OnPointerClick(PointerEventData eventData){
        player.lockMovement = true;
        player.sceneController.lockMovement = true;
    }

    public void triggerAutocomplete(string input){
        if(input != ""){
            firstDemonMatch = data.demonList.FirstOrDefault(demon => demon.StartsWith(input, System.StringComparison.OrdinalIgnoreCase));
            autocomplete.text = firstDemonMatch;
        }
        else{
            autocomplete.text = "";
        }
    }

    public void clearAutocomplete(string input){
        latestGuess = input;

        autocomplete.text = "";
        inputField.text = "";

        player.lockMovement = false;
        player.sceneController.lockMovement = false;
    }

    public void enterGuess(){
        validDemon = string.Compare(latestGuess, firstDemonMatch, true) == 0;

        if(validDemon){
            Debug.Log("entered valid demon");
        }
        else{
            Debug.Log("did not enter valid demon");
        }
    }
}
