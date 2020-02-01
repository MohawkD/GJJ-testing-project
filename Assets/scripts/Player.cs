using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber = 1;
    public Sprite[] buttonImages;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Random.Range(0, 3));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("P" + playerNumber + " A")) {
            Debug.Log("P" + playerNumber + " A");
        }

        if(Input.GetButton("P" + playerNumber + " X")) {
            Debug.Log("P" + playerNumber + " X");
        }

        if(Input.GetButton("P" + playerNumber + " B")) {
            Debug.Log("P" + playerNumber + " B");
        }
        if(Input.GetButton("P" + playerNumber + " Y")) {
            Debug.Log("P" + playerNumber + " Y");
        }
    }
}
