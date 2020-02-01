using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerNumber = 1;
    public GameObject[] buttonImages;
    private int[] requiredInputs = new int[3];
    private int[] givenInputs = new int[3];
    public Vector2Int currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move());
    }


    private IEnumerator waitForKeyPresses()
    {
        bool done = false;
        int index = 0;
        while(!done) // essentially a "while true", but with a bool to break out naturally
        {
            int button_pressed = -1;
            if(Input.GetButtonDown("P" + playerNumber + " A")) {
                button_pressed = 0;
                Debug.Log("P" + playerNumber + " A");
            } else if(Input.GetButtonDown("P" + playerNumber + " B")) {
                button_pressed = 1;
                Debug.Log("P" + playerNumber + " B");
            } else if(Input.GetButtonDown("P" + playerNumber + " X")) {
                button_pressed = 2;
                Debug.Log("P" + playerNumber + " X");
            } else if(Input.GetButtonDown("P" + playerNumber + " Y")) {
                button_pressed = 3;
                Debug.Log("P" + playerNumber + " Y");
            }

            if(button_pressed >= 0)
            {
                givenInputs[index] = button_pressed;
                index++;
                if(index > 2) {
                    done = true;
                    Debug.Log("P" + playerNumber + " player input received");
                }
            }
            yield return null;
        }
    }

    private IEnumerator Move()
    {
        while(true) {
            DisplayInputs();

            yield return waitForKeyPresses();

            int correctInputs = 0;
            for(int i = 0; i < 3; i++) {
                if(givenInputs[i] == requiredInputs[i]) {
                    correctInputs++;
                }
            }
            
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
            yield return new WaitForSeconds(1);
        }
    }

    private void DisplayInputs()
    {
        for(int i = 0; i < 3; i++) {
            //int button_index = Random.Range(0, 3);
            int button_index = i;
            requiredInputs[i] = button_index;
            DisplayButton(button_index, i);
        }
    }

    private void DisplayButton(int button_index, int position_index)
    {
        buttonImages[button_index].SetActive(true);
        buttonImages[button_index].transform.localPosition = new Vector3(20 * position_index, 0, 0);
    
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetButtonDown("P" + playerNumber + " A")) {
            Debug.Log("P" + playerNumber + " A");
        }

        if(Input.GetButtonDown("P" + playerNumber + " X")) {
            Debug.Log("P" + playerNumber + " X");
        }

        if(Input.GetButtonDown("P" + playerNumber + " B")) {
            Debug.Log("P" + playerNumber + " B");
        }
        if(Input.GetButtonDown("P" + playerNumber + " Y")) {
            Debug.Log("P" + playerNumber + " Y");
        }
        */
    }
}
