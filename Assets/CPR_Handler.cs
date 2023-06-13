using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPR_Handler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject GameManager;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        GameManager = GameObject.Find("GameManager");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerIsNearPatient())
        {
            animator.SetBool("Dying", true);
            animator.SetBool("isLying", true);
        }
    }

    public void HandleCPR()
    {
        
    }

    private bool PlayerIsNearPatient()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= 5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
