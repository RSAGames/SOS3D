using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CprPatient : MonoBehaviour
{
    private Collider Collider;
    private GameObject MessageBoard;
    [SerializeField] private TextMeshProUGUI MessageBoardText;
    
    private void Awake()
    {
        Collider = GetComponent<Collider>();
        Collider.isTrigger = true;
    }

    private void Start()
    {
        MessageBoard = GameObject.Find("Canvas/MessageBoard");
        MessageBoardText = GameObject.Find("Canvas/MessageBoard/Text").GetComponent<TextMeshProUGUI>();

        if (MessageBoard != null)
        {
            MessageBoard.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            MessageBoardText.text = "This person is having a heart attack";
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
                Debug.Log("Triggered Exit");
            }
        }
    }
}
