using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Portal : MonoBehaviour
{

    private Collider Collider;
    [SerializeField] private InputAction Enter;
    [SerializeField] private GameObject MessageBoard;
    [SerializeField] private GameObject VideoPlayer;

    private void Awake()
    {
        Collider = GetComponent<Collider>();
        Collider.isTrigger = true;
    }

    private void Start()
    {
        if (MessageBoard == null)
        {
            MessageBoard = GameObject.Find("Canvas/MessageBoard");
            Debug.Log("Could not find MessageBoard");
        }
        else
        {
            Debug.Log("Found MessageBoard");
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
        if (Enter == null)
        {
            Enter = new InputAction("Enter", binding: "<Keyboard>/e");
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MessageBoard.SetActive(true);
            Debug.Log("Triggered");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MessageBoard.SetActive(false);
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
                // Play video
                VideoPlayer.SetActive(true);
            }
        }
    }
}
