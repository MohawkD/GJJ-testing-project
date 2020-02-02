using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine.UI;

public class ScenesController : MonoBehaviour
{
    public UIView View;

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game 1");
        
        View.Hide();
        //Destroy(this.gameObject);
//        Application.Quit();
    }

    public void RestartGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Finish");
        //Destroy(this.gameObject);
    }
}
