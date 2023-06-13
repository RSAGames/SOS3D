using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPR_Handler : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject GameManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        GameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator HandleCPR()
    {
        Debug.Log("CPR started");
        yield return new WaitForSeconds(5);
        Debug.Log("CPR ended");
    }
}
