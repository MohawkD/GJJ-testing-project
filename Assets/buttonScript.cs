using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Doozy.Engine.UI;

public class buttonScript : MonoBehaviour
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
        SceneManager.LoadScene("Game 1");
        View.Hide();
    }
}
