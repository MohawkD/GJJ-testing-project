﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerNumber = 1;
    public Button[] buttons;
    public Sprite[] buttonImages;
    public int bounceAmount = 2;
    public int bounceBorder = 5;
    public int winDistance = 3;
    private int[] requiredInputs = new int[3];
    private int[] givenInputs = new int[3];
    public Vector2Int currentPosition;
    public Player otherPlayer;
    public GameObject wirePrefab;

    public float Speed;
    private Wire currentWire = null;


    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
        Invoke("startGame", 2.0f);
    }


    private void startGame() {
        foreach(Button button in buttons) {
            button.gameObject.SetActive(true);
        }
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

    private Vector2Int checkIfBounce(Vector2Int newPosition, Vector2Int moveDirection, out bool isBouncing)
    {
        isBouncing = false;
        if(Mathf.Abs(newPosition.x) > bounceBorder || Mathf.Abs(newPosition.y) > bounceBorder)
        {
            isBouncing = true;
            moveDirection = -1 * bounceAmount * moveDirection;
            newPosition = currentPosition + moveDirection;
            Debug.Log("Player " + playerNumber + " tried of move out of bounds and bounced");
        }
        if(newPosition == otherPlayer.currentPosition)
        {
            isBouncing = true;
            moveDirection = -1 * bounceAmount * moveDirection;
            newPosition = currentPosition + moveDirection;
            Debug.Log("Player " + playerNumber + " tried of move on another player's tile and bounced");
        }
        return newPosition;
    }

    private bool checkIfWin(Vector2Int currentPosition) {
        if(currentPosition.x >= winDistance) {
            foreach(Button button in buttons) {
                button.gameObject.SetActive(false);
            }
            foreach(Button button in otherPlayer.buttons) {
                button.gameObject.SetActive(false);
            }
            StopCoroutine(Move());
            //TODO: winning animation
            Debug.Log("Player " + playerNumber + " wins!");
            return true;
        }
        return false;
    }

    private Vector2Int findMoveDirection() {
        int correctInputs = 0;
        for(int i = 0; i < 3; i++) {
            if(givenInputs[i] == requiredInputs[i]) {
                correctInputs++;
            }
        }
        Debug.Log("correct inputs: " + correctInputs);
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

        bool adjustGridCoordinate = correctInputs == 2 || correctInputs == 1;
        if(adjustGridCoordinate && currentPosition.y % 2 == 0) {
            moveDirection = moveDirection + Vector2Int.left; 
        }

        return moveDirection;
    }

    private IEnumerator Move()
    {
        bool hasPlayerWon = false;
        while(!hasPlayerWon) {
            //move stop
            animator.SetTrigger(GlobalVar.finishMoving);
            
            DisplayInputs();
            yield return waitForKeyPresses();

            Vector2Int moveDirection = findMoveDirection();
            Vector2Int newPosition = currentPosition + moveDirection;
            bool isBouncing = false;
            newPosition = checkIfBounce(newPosition, moveDirection, out isBouncing);
            
            currentPosition = newPosition;
            Debug.Log("New pos: " + currentPosition);
            
            hasPlayerWon = checkIfWin(currentPosition);

            GameObject go = Instantiate(wirePrefab, this.transform.position, Quaternion.identity);
            currentWire = go.GetComponent<Wire>();

            if (isBouncing)
            {
                //bounce anim
                animator.SetTrigger(GlobalVar.startBouncing);
            }
            else
            {
                //move anim
                animator.SetTrigger(GlobalVar.startMoving);
            }
            
            yield return new WaitForSeconds(GlobalVar.waitTimeEachMove);
        }
    }

    private void DisplayInputs()
    {
        for(int i = 0; i < 3; i++) {
            int button_index = Random.Range(0, 4);
            requiredInputs[i] = button_index;
            buttons[i].GetComponent<Image>().sprite = buttonImages[button_index];
        }
    }

    public void UpdatePosition(Vector3 pos)
    {
        float step = Speed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, pos, step);
        
        if (currentWire != null) 
        {
            currentWire.IncreaseTo(transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
