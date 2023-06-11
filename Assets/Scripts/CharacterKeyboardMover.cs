using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterKeyboardMover : MonoBehaviour
{
    [SerializeField] float speed = 3.5f;
    [SerializeField] float rotationSpeed = 180f;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpHeight = 1.5f;

    private int frameCount = 0;
    private CharacterController cc;
    private Animator animator;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    [SerializeField] InputAction runningAction;
    [SerializeField] InputAction cprAction;

    private Coroutine cprCoroutine;

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        jumpAction.canceled += OnButtonReleased;
        runningAction.Enable();
        cprAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        jumpAction.canceled -= OnButtonReleased;
        runningAction.Disable();
        cprAction.Disable();
    }

    void OnValidate()
    {
        // ...
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("CharacterKeyboardMover requires an Animator component on the same GameObject");
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

    Vector3 velocity = Vector3.zero;

    IEnumerator HandleCPR()
    {
        // make a 120 sec timer checking for player space key presses and adjusting animator speed accordingly
        frameCount = 0;      
        float startTime = Time.time;
        float endTime = startTime + 120f;
        animator.speed = 1f;
        moveAction.Disable();
        while (Time.time < endTime)
        {
            if (jumpAction.triggered)
            {
                Debug.Log("CPR Action Triggered");
                if (animator.GetBool("CPR")){
                Debug.Log("Button released increasing speed");
                animator.speed += 0.1f;
        }
            }
            else if (animator.speed > 0.6f && frameCount % 60 == 0)
            {
                Debug.Log("Reducing speed :" + animator.speed + " FrameCount = " + frameCount);
                animator.speed -= 0.1f;
            }
            frameCount++;
            yield return null;
        }
        moveAction.Enable();
    }

    void FixedUpdate()
    {
        if (cc.isGrounded)
        {
            // Handle rotation
            float rotationInput = moveAction.ReadValue<Vector2>().x; // Read the 'A' and 'D' keys
            float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, rotationAmount, 0f);

            Vector2 movement = moveAction.ReadValue<Vector2>();
            float currentSpeed = speed;

            if (runningAction.IsPressed() && movement.magnitude > 0)
            {
                currentSpeed *= 2f;
                animator.SetBool("isRunning", true);
                animator.SetBool("isWalking", false);
            }
            else
            {
                currentSpeed /= 2f;
                animator.SetBool("isRunning", false);
                animator.SetBool("isWalking", true);
            }

            if (movement.magnitude > 0)
            {
                Vector3 move = transform.forward * movement.y;
                move *= currentSpeed;
                velocity = move;
            }
            else
            {
                velocity = Vector3.zero;
                animator.SetBool("isWalking", false);
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        if (jumpAction.triggered && cc.isGrounded && !animator.GetBool("CPR"))
        {
            animator.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);
        }

        if (cprAction.triggered)
        {
            if (cprCoroutine != null)
            {
                StopCoroutine(cprCoroutine);
            }
            cprCoroutine = StartCoroutine(HandleCPR());
            animator.SetBool("CPR", true);
        }

        cc.Move(velocity * Time.deltaTime);
    }
}
