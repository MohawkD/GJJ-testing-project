using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinText : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;

    // Start is called before the first frame update
    void Start()
    {
        int winner_number = PlayerPrefs.GetInt("winner_number");
        TextMeshProUGUI textmeshPro1 = text1.GetComponent<TextMeshProUGUI>();
        textmeshPro1.SetText("PLAYER " + winner_number + " WINS");

        TextMeshProUGUI textmeshPro2 = text2.GetComponent<TextMeshProUGUI>();
        textmeshPro2.SetText("PLAYER " + winner_number + " WINS");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
