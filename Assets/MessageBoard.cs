using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoard : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private GameObject messageBoard;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        DisableMessageBoard();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableMessageBoard(string message)
    {
        gameObject.GetComponent<Image>().enabled = true;
        text.enabled = true;
        text.text = message;
    }

    public void DisableMessageBoard()
    {
        gameObject.GetComponent<Image>().enabled = false;
        text.enabled = false;
        text.text = "";
    }

    public void UpdateMessage(string message)
    {
        text.text = message;
    }
}


