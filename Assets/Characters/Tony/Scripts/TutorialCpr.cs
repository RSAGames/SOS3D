using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;

public class TutorialCpr : MonoBehaviour
{
    [SerializeField] private GameObject rightClick;
    [SerializeField] private GameObject wrongClick;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Animator patientAnimator;
    [SerializeField] private GameObject patient;
    [SerializeField] private Vector3 patientOffset = new Vector3(0.14f, 0f , 0.04f);
    [SerializeField] private Vector3 RotationOffset = new Vector3(0f, 0f, 0f);
    [SerializeField] private int requiredClicks = 100;
    [SerializeField] private int maxClicks = 120;
    [SerializeField] private InputAction push = new InputAction(type: InputActionType.Button);
    [SerializeField] private float timer = 0f;
    [SerializeField] private float maxTime = 15f;
    [SerializeField] private int clickCount = 0;
    [SerializeField] private Vector3 targetPosition;
    private bool isResuscitating = false;
    private bool isSuccess = false;
    private float startTime;
    private float clicksPerMin;
    private IEnumerator coroutine;
    private AudioSource callingAudioSource;
    private bool startResuscitationOnce = true;
    [SerializeField] private GameObject MessageBoard;

    private CharacterController characterController;

    void Start()
    {
        MessageBoard = GameObject.Find("Canvas/MessageBoard");
        characterController = GetComponent<CharacterController>();
        patientAnimator = patient.GetComponent<Animator>();
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
        if (push.triggered)
        {
            if (startResuscitationOnce && PlayerIsNearPatient())
            {
                StartResuscitation();
                startResuscitationOnce = false;
                startTime = Time.time;
            }
            float timeElapsed = Time.time - startTime;
            clickCount++;
            clicksPerMin = Mathf.RoundToInt((clickCount / timeElapsed) * 60);
            MessageBoard.GetComponent<MessageBoard>().UpdateMessage("Clicks per minute: " + clicksPerMin);
        }

        if (isResuscitating)
        {
            timer += Time.deltaTime;
            if (timer >= maxTime)
            {
                EndResuscitation();
            }
        }
    }

    private void StartResuscitation()
    {
        Debug.Log("Start Resuscitation");
        transform.GetComponent<CharacterKeyboardMover>().enabled = false;
        StartCoroutine(MoveToPatientCoroutine());
    }

  private IEnumerator MoveToPatientCoroutine()
{
    
    playerAnimator.SetBool("isWalking", true);
    GameObject heart = patient.transform.Find("Heart").gameObject;
    // Get patient heart position by his child object called Heart
    if (heart == null)
    {
        Debug.LogError("Patient does not have a child object called Heart");
    }
    else {
        targetPosition = heart.transform.position;
        Debug.Log("Target position: " + targetPosition);
        }
    Quaternion targetRotation = heart.transform.rotation;

    while (Vector3.Distance(transform.position, targetPosition) > 0.05f || Quaternion.Angle(transform.rotation, targetRotation) > 0.05f)
    {
        if (Vector3.Distance(transform.position, targetPosition) <= 0.05f)
        {
            playerAnimator.SetBool("isWalking", false);
        }
        transform.position = Vector3.Lerp(transform.position, targetPosition, 1f * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
        yield return null;
    }

    playerAnimator.SetBool("isWalking", false);

    patientAnimator.SetBool("ReceivingCPR", true);
    playerAnimator.SetBool("CPR", true);

    isResuscitating = true;
    startTime = Time.time;
}



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
            MessageBoard.GetComponent<MessageBoard>().UpdateMessage("CPR succeeded. You did " + clicksPerMin.ToString() + " clicks per minute.");
            patientAnimator.SetBool("ReceivingCPR", false);
            patientAnimator.SetBool("StandUp", true);
        }
        else
        {
            MessageBoard.GetComponent<MessageBoard>().UpdateMessage("CPR failed. You did " + clicksPerMin.ToString() + " clicks per minute.");
            patientAnimator.SetBool("ReceivingCPR", false);
            patientAnimator.SetBool("StandUp", false);
            Life_Manager.hasFailed = true;
        }
        rightClick.SetActive(false);
        wrongClick.SetActive(false);
        transform.GetComponent<CharacterKeyboardMover>().enabled = true;
    }

    private bool PlayerIsNearPatient()
    {
        return Vector3.Distance(transform.position, patient.transform.position) < 5f;
    }


}

