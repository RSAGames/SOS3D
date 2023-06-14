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

    private void Update()
    {
        isPlayerNear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // gameObject.GetComponent<Animator>().SetBool("Dying", true);
            // gameObject.GetComponent<Animator>().SetBool("isLying", true);
            // // if animator current animation is lying down put message on screen
            // if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Lying") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            // {
            //     MessageBoard.GetComponent<MessageBoard>().EnableMessageBoard("This person is having an heart attack. \n Press C to start CPR.");
            // }
            // // Debug.Log("Triggered");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //  if (other.gameObject.tag == "Player")
        // {
        //     MessageBoard.GetComponent<MessageBoard>().DisableMessageBoard();
        //     // Debug.Log("Triggered");
        // }
    }

    public void isPlayerNear(){
        GameObject player = GameObject.Find("Player");
        Vector3 playerPosition = player.transform.position;
        Vector3 patientPosition = gameObject.transform.position;
        float distance = Vector3.Distance(playerPosition, patientPosition);
        if (distance < 5f)
        {
            gameObject.GetComponent<Animator>().SetBool("Dying", true);
            gameObject.GetComponent<Animator>().SetBool("isLying", true);
            // if animator current animation is lying down put message on screen
            if (gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Lying") && gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                MessageBoard.GetComponent<MessageBoard>().EnableMessageBoard("This person is having an heart attack. \n Press C to start CPR.");
            }        }
        else
        {
            // MessageBoard.GetComponent<MessageBoard>().DisableMessageBoard();
            gameObject.GetComponent<Animator>().SetBool("Dying", false);
            gameObject.GetComponent<Animator>().SetBool("isLying", false);
        }

    }


    

    
}
