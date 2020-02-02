using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine.UI;

public class menuButtonScript : MonoBehaviour
{
    public UIView View;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene() {
        View.Hide();

        SceneManager.LoadScene("MainMenu");
    }
}
