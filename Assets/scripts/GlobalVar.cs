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

}
