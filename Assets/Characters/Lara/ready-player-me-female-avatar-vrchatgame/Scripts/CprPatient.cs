using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CprPatient : MonoBehaviour
{
    private Collider Collider;
    [SerializeField]
    private GameObject MessageBoard;
    [SerializeField]
    private TextMeshProUGUI MessageBoardText;
    [SerializeField]

    private void Awake()
    {
        Collider = GetComponent<Collider>();
        Collider.isTrigger = true;
    }

    private void Start()
    {
        MessageBoard = GameObject.Find("Canvas/MessageBoard");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponent<Animator>().SetBool("Dying", true);
            gameObject.GetComponent<Animator>().SetBool("isLying", true);
            MessageBoard.GetComponent<MessageBoard>().EnableMessageBoard("This person is having an heart attack. \n Press C to start CPR.");
            // Debug.Log("Triggered");
        }

    }

    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "Player")
        {
            MessageBoard.GetComponent<MessageBoard>().DisableMessageBoard();
            // Debug.Log("Triggered");
        }
    }

    

    
}
