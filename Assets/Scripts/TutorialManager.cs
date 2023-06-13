using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// Define a class that handles the tutorial scene 
public class TutorialManager : MonoBehaviour
{
    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    [SerializeField] InputAction runningAction;
    [SerializeField] InputAction cprAction;
    [SerializeField] private GameObject MessageBoard;
    [SerializeField] private TextMeshProUGUI MessageBoardText;
    [SerializeField] string triggeringTag;
    private bool flag = true;
    [SerializeField] private string sceneName;
    private IEnumerator coroutine;

    void Start(){
        MessageBoard.SetActive(true);
        MessageBoardText.text = "Use A,S,D,W keys to move";
    }

    void OnValidate()
    {
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Button);
        if (moveAction.bindings.Count == 0)
            moveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/W")
                .With("Down", "<Keyboard>/S")
                .With("Left", "<Keyboard>/A")
                .With("Right", "<Keyboard>/D");
        if (jumpAction == null)
            jumpAction = new InputAction(type: InputActionType.Button);
        if (jumpAction.bindings.Count == 0)
            jumpAction.AddBinding("<Keyboard>/space");
        if (runningAction == null)
            runningAction = new InputAction(type: InputActionType.Button);
        if (runningAction.bindings.Count == 0)
            runningAction.AddBinding("<Keyboard>/leftShift");
        if (cprAction == null)
            cprAction = new InputAction(type: InputActionType.Button);
        if (cprAction.bindings.Count == 0)
            cprAction.AddBinding("<Keyboard>/c");
    }

    void OnEnable()
    {
        // Enable all InputActions when the script is enabled
        moveAction.Enable();
        jumpAction.Enable();
        runningAction.Enable();
        cprAction.Enable();
    }

    void OnDisable()
    {
        // Disable all InputActions when the script is disabled
        moveAction.Disable();
        jumpAction.Disable();
        runningAction.Disable();
        cprAction.Disable();
    }

    // Update is called once per frame
    void Update(){
        Vector2 movement = moveAction.ReadValue<Vector2>();
        if (runningAction.IsPressed() && movement.magnitude > 0){
            MessageBoardText.text = "Go to the patient";
        }
        else if (jumpAction.IsPressed()){
            MessageBoardText.text = "Use Left Shift key to run";
        }
        else if(movement.magnitude > 0){
            if(flag){ // Uses bool variable for changing the text only once when player presses one of the 4 keys to move 
                MessageBoardText.text = "Use space key to jump";
                flag = false;
            }
        }
        else if (cprAction.IsPressed()){
            coroutine = loadMainScene();
            StartCoroutine(coroutine);    
        }
    }

    private IEnumerator loadMainScene()
    {
        yield return new WaitForSeconds(61);
        SceneManager.LoadScene(sceneName); 
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CprPatient")
        {
            MessageBoardText.text = "This person has heart attack, use the C key to make cpr";
            Debug.Log("Triggered");
        }

    }

    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "CprPatient")
        {
            if (MessageBoard != null)
            {
                MessageBoard.SetActive(false);
                MessageBoardText.text = "";
                Debug.Log("Triggered Exit");
            }
        }
    }
}
