using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public TextAsset _inkJsonAsset;
    private Story _story;
    public Text misterText, playerText, sisterText, currentTextField;
    public GameObject misterPanel, sisterPanel, playerPanel;
    // public Text _nameField;
    // public Color you, me, narrator;

    public GameObject _choiceButtonContainer;
    public Button _choiceButtonPrefab;
    public float delay = 0.1f;
    // public GameObject effects;

    public string activeNPC;
    public bool activeConvo = false;

    void Start(){
        _story = new Story(_inkJsonAsset.text);
        RefreshChoiceView();
        activeNPC = "";
        currentTextField = playerText;
        ClearPanels();
        // _choiceButtoncontainer 
        // DisplayNextLine();
        // effects.SetActive(false);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            // DisplayNextLine();
            
            if (activeNPC == "") {
                Debug.Log("there is no one to talk to here");
            }
            else { //if we are talking to someone, jump to that knot in the ink file
                if (!activeConvo) {
                    _story.ChoosePathString(activeNPC);
                    activeConvo = true;
                    Debug.Log("starting convo with " + activeNPC);
                }
                DisplayNextLine();
            }
        }

        // enable and disable movement during convo

    }

    public void DisplayNextLine()
    {
        ClearPanels();   
        // Color currentColor = Color.white;
        if (_story.canContinue) {
            // _continueButton.gameObject.SetActive(true);
            string text = _story.Continue(); // gets next line
            text = text?.Trim(); // removes white space from text
            
            

            List<string> tags = _story.currentTags;
            
            // if (tags.Contains("you")) {
            //     // _nameField.text = "You";
            //     currentColor = you;
            // }
            // else if (tags.Contains("me")) {
            //     // _nameField.text = "Me";
            //     currentColor = me;
            // }
            // else {
            //     // _nameField.text = "Narrator";
            //     currentColor = narrator;
            // }

            // if (_story.state.currentPathString == "sister") {
            //     currentTextField = sisterText;
            // } else if (_story.state.currentPathString == "mister"){
            //     currentTextField = misterText;
            // }
            if (tags.Contains("sister")) {
                currentTextField =  sisterText;
                sisterPanel.SetActive(true);
            } else if (tags.Contains("mister")){
                currentTextField = misterText;
                misterPanel.SetActive(true);
            } else {
                currentTextField = playerText;
                playerPanel.SetActive(true);
            }

            currentTextField.text = text;
            // _textField.text = text; // displays new text
            // _textField.text = "";
            // StartCoroutine(DisplayDialogue(text)); // typewriter effect
            // OR
            // DOTween fade in
            // _textField.DOFade(0,0);
            // _textField.DOFade(1,1);

            // if the variable "choice" changes, do something
            // _story.ObserveVariable ("choice", (string varName, object newValue) => {
            //     Debug.Log("choice was made: " + newValue);
            //     // effects.SetActive(true);
            //     StartCoroutine(Waiting(3));
                
            // });
        }
        else if (_story.currentChoices.Count > 0){
            // _continueButton.gameObject.SetActive(false);
            // _nameField.text = "Me";
            // currentColor = me;
            // _textField.text = "";
            playerPanel.SetActive(true);
            currentTextField.text = "";
            
            // Debug.Log("choices are not implemented yet");
            DisplayChoices();
        } else {
            activeConvo = false;
            Debug.Log("this convo is over");
        }
        // _dialogueBG.GetComponent<Image>().color = currentColor;
    }

    // IEnumerator DisplayDialogue(string dialogue){
    //     for (int i = 0; i < dialogue.Length; i++){
    //         // _textField.text = dialogue.Substring(0,i);
    //         currentTextField.text = dialogue.Substring(0,i);
    //         yield return new WaitForSeconds(delay);
    //     }
    // }

    // IEnumerator Waiting (float seconds){
    //     yield return new WaitForSeconds(seconds);
    //     // effects.SetActive(false);
    // }


    private void DisplayChoices()
    {
        Sequence buttonSequence = DOTween.Sequence();
        // checks if choices are already being displaye
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _story.currentChoices.Count; i++) // iterates through all choices
        {

            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text); // creates a choice button

            button.onClick.AddListener(() => OnClickChoiceButton(choice));

            button.transform.DOScale(0f, 0f);
            buttonSequence.Insert(i*0.3f,button.transform.DOScale(1f, 0.3f));

            // select the first button
            if (i == 0) StartCoroutine(SelectButton(button.gameObject));
        }
        buttonSequence.Play();
    }

    IEnumerator SelectButton(GameObject button){
        yield return null;
        EventSystem.current.SetSelectedGameObject(button);
    }

    Button CreateChoiceButton(string text)
    {
        // creates the button from a prefab
        var choiceButton = Instantiate(_choiceButtonPrefab);
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);
        
        // sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<Text>();
        buttonText.text = text;

        return choiceButton;
    }

    void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
        RefreshChoiceView(); // removes choices from the screen
        DisplayNextLine();

    }

    void RefreshChoiceView()
    {
        if (_choiceButtonContainer != null)
        {
            foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
            {
            Destroy(button.gameObject);
            }
        }
    }

    void ClearPanels(){
        misterPanel.SetActive(false);
        sisterPanel.SetActive(false);
        playerPanel.SetActive(false);
    }
}
