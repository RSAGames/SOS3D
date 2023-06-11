using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class CPR : MonoBehaviour
{
    [SerializeField] private GameObject rightClick;
    [SerializeField] private GameObject wrongClick;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator patientAnimator;
    [SerializeField] private int requiredClicks = 100;
    [SerializeField] private int maxClicks = 120;
    [SerializeField] private InputAction push = new InputAction(type: InputActionType.Button);
    // [SerializeField] private string triggeringTag;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float maxTime = 60f;
    [SerializeField] private int clickCount = 0;
    private bool isResuscitating = false;
    private bool isSuccess = false;
    private float startTime;
    private float clicksPerMin;
    private IEnumerator coroutine;
    private AudioSource callingAudioSource;
    private bool startResuscitationOnce = true;
    [SerializeField]
    private TextMeshProUGUI MessageBoardText;

    void start(){
        MessageBoardText.text = "";
    }

    void OnEnable()
    {
        // Enable all InputActions when the script is enabled
        push.Enable();
    }

    void OnDisable()
    {
        // Disable all InputActions when the script is disabled
        push.Disable();
    }

    void Update()
    {
  
        if (push.triggered){
            if(startResuscitationOnce){
                StartResuscitation();  
                startResuscitationOnce = false;
            }
            // Calculate the number of clicks per minute the player is performing
            float timeElapsed = Time.time - startTime;
            clickCount++;
            clicksPerMin = Mathf.RoundToInt((clickCount / timeElapsed) * 60);
            // Update the result text to display the clicks per minute and whether it's within the required range
            MessageBoardText.text = "You need to do between 100 to 120 clicks per minute\n Clicks per minute: " + clicksPerMin.ToString();
            // Show the appropriate feedback image based on whether the clicks per minute is within the required range or not
            // if (clicksPerMin >= requiredClicks && clicksPerMin <= maxClicks)
            // {
            //     rightClick.SetActive(true);
            //     wrongClick.SetActive(false);
            // }
            // else
            // {
            //     rightClick.SetActive(false);
            //     wrongClick.SetActive(true);
            // }
        }

        if (isResuscitating) // Check if the player is currently in the middle of resuscitating
        {
            timer += Time.deltaTime;
            if (timer >= maxTime) 
            {
                EndResuscitation();
            }
        }
    }

    // This function is called when the player starts resuscitation
    private void StartResuscitation()
    {
        playerAnimator.SetBool("CPR", true);
        patientAnimator.SetBool("ReceivingCPR", true);
        isResuscitating = true;
        startTime = Time.time;
    }
    
    // This function is called when the player ends resuscitation
    private void EndResuscitation()
    {
        playerAnimator.SetBool("CPR", false);
        playerAnimator.SetBool("StandUp", false);
        isResuscitating = false;
        if (clicksPerMin >= requiredClicks && clicksPerMin <= maxClicks)
        {
            isSuccess = true;
        }
        if (isSuccess)
        {
            MessageBoardText.text = "CPR successful! You did " + clicksPerMin.ToString() + " clicks per minute.";
            patientAnimator.SetBool("ReceivingCPR", false); // Disable the "ReceivingCPR" animation on the patient
            patientAnimator.SetBool("StandUp", true); // Enable the "StandUp" animation on the patient  
        }
        else
        {
            MessageBoardText.text = "CPR failed. You did " + clicksPerMin.ToString() + " clicks per minute.";
            patientAnimator.SetBool("ReceivingCPR", false); // Disable the "ReceivingCPR" animation on the patient
            patientAnimator.SetBool("StandUp", false); // Disable the "StandUp" animation on the patient
            Life_Manager.hasFailed = true;   
        }
        rightClick.SetActive(false);
        wrongClick.SetActive(false);
    }
}