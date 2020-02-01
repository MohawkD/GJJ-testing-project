using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject[] buttonImages;
    private int[] requiredInputs = new int[3];
    public Vector2Int currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        Move();
    }

    void Move()
    {
        int correctInputs = DisplayInputs();
        if(correctInputs == 3) {
            currentPosition = currentPosition + Vector2Int.right;
        } else if(correctInputs == 2) {
            currentPosition = currentPosition + Vector2Int.one;
        } else if(correctInputs == 1) {
            currentPosition = currentPosition + Vector2Int.up;
        } else {
            currentPosition = currentPosition + Vector2Int.left;
        }
        Debug.Log("correct inputs: " + correctInputs + ". New pos: " + currentPosition);
    }

    int DisplayInputs()
    {
        
        for(int i = 0; i < 3; i++) {
            //int button_index = Random.Range(0, 3);
            int button_index = i;
            requiredInputs[i] = button_index;
            DisplayButton(button_index, i);
        }
        int correctInputs = Random.Range(0, 3);
        return(correctInputs);
    }

    void DisplayButton(int button_index, int position_index)
    {
        buttonImages[button_index].SetActive(true);
        buttonImages[button_index].transform.position = new Vector3(buttonImages[button_index].transform.position.x + (20 * position_index), buttonImages[button_index].transform.position.y, buttonImages[button_index].transform.position.z);
    
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
