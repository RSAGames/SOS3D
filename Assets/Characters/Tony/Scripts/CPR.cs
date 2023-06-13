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
    [SerializeField] private GameObject patient;
    [SerializeField] private Vector3 patientOffset = new Vector3(2f, 0f, 2f);
    [SerializeField] private int requiredClicks = 100;
    [SerializeField] private int maxClicks = 120;
    [SerializeField] private InputAction push = new InputAction(type: InputActionType.Button);
    [SerializeField] private float timer = 0f;
    [SerializeField] private float maxTime = 15f;
    [SerializeField] private int clickCount = 0;
    private bool isResuscitating = false;
    private bool isSuccess = false;
    private float startTime;
    private float clicksPerMin;
    private IEnumerator coroutine;
    private AudioSource callingAudioSource;
    private bool startResuscitationOnce = true;
    [SerializeField] private TextMeshProUGUI MessageBoardText;

    private CharacterController characterController;

    void Start()
    {
        MessageBoardText.text = "";
        characterController = GetComponent<CharacterController>();
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
            if (startResuscitationOnce)
            {
                StartResuscitation();
                startResuscitationOnce = false;
            }
            float timeElapsed = Time.time - startTime;
            clickCount++;
            clicksPerMin = Mathf.RoundToInt((clickCount / timeElapsed) * 60);
            MessageBoardText.text = "You need to do between 100 to 120 clicks per minute\n Clicks per minute: " + clicksPerMin.ToString();
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
        StartCoroutine(MoveToPatientCoroutine());
    }

    private IEnumerator MoveToPatientCoroutine()
    {
        playerAnimator.SetBool("isWalking", true);

        Vector3 targetPosition = patient.transform.position + patientOffset;

        while (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            characterController.Move(direction * Time.deltaTime * 3f); // Adjust the speed as needed
            Debug.Log("Position: " + transform.position + " Target: " + targetPosition);
            yield return null;
        }

        playerAnimator.SetBool("isWalking", false);
        playerAnimator.SetBool("CPR", true);

        isResuscitating = true;
        startTime = Time.time;
    }

    private void EndResuscitation()
    {
        playerAnimator.SetBool("CPR", false);
        playerAnimator.SetBool("StandUp", false);
        characterController.enabled = true;
        isResuscitating = false;
        if (clicksPerMin >= requiredClicks && clicksPerMin <= maxClicks)
        {
            isSuccess = true;
        }
        if (isSuccess)
        {
            MessageBoardText.text = "CPR successful! You did " + clicksPerMin.ToString() + " clicks per minute.";
            patientAnimator.SetBool("ReceivingCPR", false);
            patientAnimator.SetBool("StandUp", true);
        }
        else
        {
            MessageBoardText.text = "CPR failed. You did " + clicksPerMin.ToString() + " clicks per minute.";
            patientAnimator.SetBool("ReceivingCPR", false);
            patientAnimator.SetBool("StandUp", false);
            Life_Manager.hasFailed = true;
        }
        rightClick.SetActive(false);
        wrongClick.SetActive(false);
    }
}
