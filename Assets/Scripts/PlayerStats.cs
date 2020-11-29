using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    private static int points;
    private static bool playerStartedGame;

    public static bool PlayerStartedGame
    {
        get { return playerStartedGame; }
        set { playerStartedGame = value; }
    }
    public static int Points
    {
        get { return points; }
        set { points = value; }
    }
    
}
