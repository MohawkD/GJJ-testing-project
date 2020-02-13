using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinScreenManager : MonoBehaviour
{
    public GameObject text1;
    public GameObject text2;

    public GameObject p1_win_anim;
    public GameObject p1_lose_anim;
    public GameObject p2_win_anim;
    public GameObject p2_lose_anim;




    // Start is called before the first frame update
    void Start()
    {
        int winner_number = PlayerPrefs.GetInt("winner_number");
        TextMeshProUGUI textmeshPro1 = text1.GetComponent<TextMeshProUGUI>();
        textmeshPro1.SetText("PLAYER " + winner_number + " WINS");

        TextMeshProUGUI textmeshPro2 = text2.GetComponent<TextMeshProUGUI>();
        textmeshPro2.SetText("PLAYER " + winner_number + " WINS");

        if(winner_number == 1) {
            p1_win_anim.SetActive(true);
            p1_lose_anim.SetActive(false);
            p2_win_anim.SetActive(false);
            p2_lose_anim.SetActive(true);
        } else if (winner_number == 2) {
            p2_win_anim.SetActive(true);
            p2_lose_anim.SetActive(false);
            p1_win_anim.SetActive(false);
            p1_lose_anim.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
