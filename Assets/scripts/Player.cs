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
    public Sprite[] wrongButtonImages;
    public Sprite[] rightButtonImages;

    public int bounceAmount = 2;
    public int bounceBorder = 5;
    public int winDistance = 10;
    public float timeFrozen = 5.0f;
    public int speedUpBoost = 3;
    
    public Vector2Int currentPosition;
    public Player otherPlayer;
//    public GameObject wirePrefab;

    public float Speed;

    private int[] requiredInputs = new int[3];
    private int[] givenInputs = new int[3];
//    private Wire currentWire = null;
    private int easyInputsDone = 0;
    private bool isSpedUp = false;
    private bool isFrozen = false;
    private int correctInputs = 0;
    
    private Animator animator;
    private NewWire wire;
    
    private void Awake()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        wire = new NewWire(lineRenderer, transform.position);
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        int registeredPresses = 0;
        correctInputs = 0;

        while(registeredPresses < 3)
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
                if(button_pressed == requiredInputs[registeredPresses]) {
                    correctInputs++;
                    buttons[registeredPresses].GetComponent<Image>().sprite = rightButtonImages[requiredInputs[registeredPresses]];
                } else {
                    buttons[registeredPresses].GetComponent<Image>().sprite = wrongButtonImages[requiredInputs[registeredPresses]];
                }
                registeredPresses++;
            }

            yield return null;
        }
        Debug.Log("P" + playerNumber + " player input received");
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

    private Vector2Int findMoveDirection(int correctInputs) {
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

    private void freezeOpponent()
    {
        otherPlayer.isFrozen = true;
    }

    private void checkPowerUp(Vector2Int currentPosition) {
        string content = powerUpSpawner.instance.getCoordinateContent(currentPosition);
        if(content == "freeze") {
            Debug.Log("Pickup freeze");
            freezeOpponent();
        } else if(content == "speed") {
            isSpedUp = true;
        }
    }

    private IEnumerator Move()
    {
        bool hasPlayerWon = false;
        while(!hasPlayerWon) {
            //move stop
            animator.SetTrigger(GlobalVar.finishMoving);
            
            DisplayInputs();
            yield return waitForKeyPresses();

            Vector2Int moveDirection = findMoveDirection(correctInputs);
            Vector2Int newPosition = currentPosition + moveDirection;
            bool isBouncing = false;
            newPosition = checkIfBounce(newPosition, moveDirection, out isBouncing);
            
            currentPosition = newPosition;
            Debug.Log("New pos: " + currentPosition);

            checkPowerUp(currentPosition);
            hasPlayerWon = checkIfWin(currentPosition);

//            GameObject go = Instantiate(wirePrefab, this.transform.position, Quaternion.identity);
//            currentWire = go.GetComponent<Wire>();

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
            
            wire.AddWireNode();
            
            yield return new WaitForSeconds(GlobalVar.waitTimeEachMove);
        }
    }

    private void DisplayInputs()
    {
        for(int i = 0; i < 3; i++) {
            buttons[i].GetComponent<Image>().color = new Color32(255,255,255,255);
        }
        int[, ] easyButtonIndices = new int[2, 3] {{0, 0, 0}, {3, 0, 3}};
        int easy_index = Random.Range(0, easyButtonIndices.GetLength(0));

        for(int i = 0; i < 3; i++) {
            int button_index = 0;
            if(!isSpedUp) {
                button_index = Random.Range(0, 4);
            } else {
                button_index = easyButtonIndices[easy_index, i];
            }
            requiredInputs[i] = button_index;
            buttons[i].GetComponent<Image>().sprite = buttonImages[button_index];
        }

        if(isSpedUp) {
            easyInputsDone++;
        }
    }

    public void UpdatePosition(Vector3 pos)
    {
        float step = Speed * Time.deltaTime;

        transform.position = Vector3.Lerp(transform.position, pos, step);
        wire.IncreaseTo(transform.position);
        
//        if (currentWire != null) 
//        {
//            currentWire.IncreaseTo(transform.position);
//        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpedUp && easyInputsDone > speedUpBoost) {
            Debug.Log("speed up over");
            easyInputsDone = 0;
            isSpedUp = false;
        }
        if(isFrozen) {
            foreach(Button button in buttons) {
                button.gameObject.SetActive(false);
            }
            //foreach(Button button in buttons) {
                //button.gameObject.SetActive(true);
            //}
        }
    }
}
