using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterKeyboardMover : MonoBehaviour
{
    [SerializeField] float speed = 3.5f;
    [SerializeField] float rotationSpeed = 180f; // New variable for rotation speed
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpHeight = 1.5f;

    private CharacterController cc;
    private Animator animator;

    [SerializeField] InputAction moveAction;
    [SerializeField] InputAction jumpAction;
    [SerializeField] InputAction runningAction;
    [SerializeField] InputAction cprAction;

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        runningAction.Enable();
        cprAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        runningAction.Disable();
        cprAction.Disable();
    }

    void OnValidate()
    {
        if (moveAction == null)
            moveAction = new InputAction(type: InputActionType.Value);
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
    Coroutine cprCoroutine;

    IEnumerator StopCPR()
    {
        animator.SetBool("CPR_Loop", true);
        yield return new WaitForSeconds(120);
        animator.SetBool("CPR_Loop", false);
        animator.SetBool("CPR", false);
    }

    void Update()
    {
        if (cc.isGrounded)
        {
            Vector2 movement = moveAction.ReadValue<Vector2>();
            float currentSpeed = speed;

            // Handle rotation
            float rotationInput = moveAction.ReadValue<Vector2>().x; // Read the 'A' and 'D' keys
            float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
            transform.forward = Quaternion.Euler(0, rotationAmount, 0) * transform.forward;
            if (runningAction.IsPressed() && movement.magnitude > 0)
            {
                currentSpeed *= 2f; // Double the speed when running
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

        if (jumpAction.triggered && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(2 * gravity * jumpHeight);
        }

        cc.Move(velocity * Time.deltaTime);
    }
}
