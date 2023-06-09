using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life_Manager : MonoBehaviour
{
    [SerializeField] 
    private int life = 3;
    [SerializeField] 
    private GameObject Lifes;
    [SerializeField]
    private bool isTriggered = false;
    [SerializeField]
    

    // Start is called before the first frame update
    void Start()
    {
       // find Lifes components
        Lifes = GameObject.Find("Lifes");
        // get only sons of the object

    }

    // Update is called once per frame
    void Update()
    {
        
        // if triggered and no life left, game over
        if (isTriggered && isDead())
        {
            Debug.Log("Game Over");
        }

        else if (isTriggered && !isDead())
        {
            Debug.Log("Life Lost");
            life--;
            isTriggered = false;
            Lifes.transform.GetChild(life).gameObject.SetActive(false);            
        }
        // if triggered and life left, remove life
    }



    private bool isDead()
    {
        if (life <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CallTrigger(GameManager gameManager)

        // check if gameManager is really a GameManager
        // if it is, call Trigger

    {
        if (gameManager is GameManager) 
        isTriggered = true;
    }
}
