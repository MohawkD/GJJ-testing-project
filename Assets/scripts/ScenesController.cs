using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    
    public void StartNewGame()
    {
        SceneManager.LoadScene("Game 1");
        
        Destroy(this.gameObject);
//        Application.Quit();
    }

    public void RestartGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Finish");
        Destroy(this.gameObject);
    }
}
