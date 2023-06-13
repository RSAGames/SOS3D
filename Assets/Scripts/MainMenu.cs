using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("TutorialScene");
    }

    public void ExitGame(){
        Debug.Log("Application is Closed");
        Application.Quit();
    }
}
