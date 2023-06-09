using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Camera_Movement : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float MovementSpeed = 0.1f;
    [SerializeField] InputAction lookLocation;

    void OnEnable() {        lookLocation.Enable();    }
    void OnDisable() {        lookLocation.Disable();   }
    

    // move camera using keyboard keys 
    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (lookLocation == null)
            lookLocation = new InputAction(type: InputActionType.Value);
        if (lookLocation.bindings.Count == 0)
            lookLocation.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/UpArrow")
                .With("Down", "<Keyboard>/DownArrow")
                .With("Left", "<Keyboard>/LeftArrow")
                .With("Right", "<Keyboard>/RightArrow");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         float mouseX = Mouse.current.delta.x.ReadValue();
         float mouseY = Mouse.current.delta.y.ReadValue();
        
        Vector3 rotation = transform.localEulerAngles;
        rotation.x -= mouseY * rotationSpeed;
        // rotation.y += mouseX * rotationSpeed;  // Rotation around the vertical (Y) axis
        transform.localEulerAngles = rotation;

        // move camera using keyboard keys
        Vector2 Movement = lookLocation.ReadValue<Vector2>();
        Vector3 move = new Vector3(Movement.x, 0, Movement.y);
        transform.Translate(move * Time.deltaTime * MovementSpeed);

    }
}
