using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerNumber = 1;
    public Button[] buttons;
    public Sprite[] buttonImages;
    public int bounce_amount = 2;
    private int[] requiredInputs = new int[3];
    private int[] givenInputs = new int[3];
    public Vector2Int currentPosition;

    public GameObject wirePrefab;

    private Wire currentWire = null;
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
            
            //for keyboard test
//            if(Input.GetKeyDown(KeyCode.A)) {
//                button_pressed = 0;
//                Debug.Log("P" + playerNumber + " A");
//            } else if(Input.GetKeyDown(KeyCode.B)) {
//                button_pressed = 1;
//                Debug.Log("P" + playerNumber + " B");
//            } else if(Input.GetKeyDown(KeyCode.X)) {
//                button_pressed = 2;
//                Debug.Log("P" + playerNumber + " X");
//            } else if(Input.GetKeyDown(KeyCode.Y)) {
//                button_pressed = 3;
//                Debug.Log("P" + playerNumber + " Y");
//            }

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
            
            Vector2Int moveDirection = Vector2Int.zero;
            if(correctInputs == 3) {
                moveDirection = Vector2Int.right;
            } else if(correctInputs == 2) {
                moveDirection = Vector2Int.one;
            } else if(correctInputs == 1) {
                moveDirection = Vector2Int.up;
            } else {
                moveDirection = Vector2Int.left;
            }
            
            Vector2Int newPosition = currentPosition + moveDirection;

            if(Mathf.Abs(newPosition.x) > 3 || Mathf.Abs(newPosition.y) > 3) {
                moveDirection = -1 * bounce_amount * moveDirection;
                newPosition = currentPosition + moveDirection;
                Debug.Log("Player " + playerNumber + " tried of move out of bounds and bounced");
            }
            currentPosition = newPosition;
            Debug.Log("correct inputs: " + correctInputs + ". New pos: " + currentPosition);
            
            GameObject go = Instantiate(wirePrefab, this.transform.position, Quaternion.identity);
            currentWire = go.GetComponent<Wire>();
            
            yield return new WaitForSeconds(1);
        }
    }

    private void DisplayInputs()
    {
        for(int i = 0; i < 3; i++) {
            int button_index = Random.Range(0, 3);
            requiredInputs[i] = button_index;
            buttons[i].GetComponent<Image>().sprite = buttonImages[button_index];
        }
    }

    public void UpdatePosition(Vector3 pos)
    {
        this.transform.position = pos;
        
        if (currentWire != null) 
        {
            currentWire.IncreaseTo(pos);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
