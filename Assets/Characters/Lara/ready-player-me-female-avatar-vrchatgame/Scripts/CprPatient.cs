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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MessageBoardText.text = "This person has heart attack";
            MessageBoard.SetActive(true);
            Debug.Log("Triggered");
        }

    }

    private void OnTriggerExit(Collider other)
    {
         if (other.gameObject.tag == "Player")
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
