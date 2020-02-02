using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalVar
{
    //global var for player control
    //unit: second
    public static float waitTimeEachMove = 1;
    
    //animator parameters
    public static string startMoving = "startMoving";
    public static string finishMoving = "finishMoving";
    public static string startWinningGame = "startWinningGame";
    public static string startLosingGame = "startLosingGame";
    public static string endGame = "endGame";
    public static string startBouncing = "startBouncing";
    public static string startPowerUp = "startPowerUp";
    public static string finishPowerUp = "finishPowerUp";

    public static string isMoving = "isMoving";

    public static int NumOfMoveDirections = 6;

    public static Vector2Int GetMoveDirection(int moveIndex, Vector2Int currentPos)
    {
        List<Vector2Int> directionListOdd = new List<Vector2Int>
        {
            Vector2Int.right, 
            Vector2Int.one, 
            Vector2Int.up, 
            Vector2Int.left,
            new Vector2Int(0,-1), 
            new Vector2Int(1, -1)
        };
        
        List<Vector2Int> directionListEven = new List<Vector2Int>
        {
            Vector2Int.right, 
            Vector2Int.up, 
            new Vector2Int(-1, 1), 
            Vector2Int.left,
            -Vector2Int.one, 
            Vector2Int.down
        };

        //positive negative issue
        if (Math.Abs(currentPos.y) % 2 == 1)
        {
            return directionListOdd[moveIndex];
        }
        else
        {
            return directionListEven[moveIndex];
        }
        
    }

    public static int GetNextMoveDirectionIndex(int preMoveIndex, int numofIncorrect)
    {
        int newDirect = (preMoveIndex + numofIncorrect) % NumOfMoveDirections;
        return newDirect;
    }
}
