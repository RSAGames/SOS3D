using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Portal : MonoBehaviour
{

    private Collider Collider;
    [SerializeField] private InputAction Enter;
    [SerializeField] private GameObject panel;
    private GameObject text;
    private void Awake()
    {
        Collider = GetComponent<Collider>();
        Collider.isTrigger = true;
        
    }

    private void Start()
    {
        if (panel == null)
        {
            panel = GameObject.Find("Canvas/MessageBoard");
            Debug.Log("Could not find panel");
        }
        else
        {
            Debug.Log("Found panel");
        }
    }

    private void OnEnable()
    {
        Enter.Enable();
    }

    private void OnDisable()
    {
        Enter.Disable();
    }

    private void OnValidate()
    {
        if(Enter == null)
        {
            Enter = new InputAction("Enter", binding: "<Keyboard>/e");
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            panel.SetActive(true);
            Debug.Log("Triggered");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            panel.SetActive(false);
            Debug.Log("Triggered Exit");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Debug.Log("Triggered Stay");
        if (other.gameObject.tag == "Player")
        {
            if (Enter.triggered)
            {
                Debug.Log("Enter triggered");
                // Teleport player to next level
            }
        }
        // Display messsage to press E to enter portal
    }
}
