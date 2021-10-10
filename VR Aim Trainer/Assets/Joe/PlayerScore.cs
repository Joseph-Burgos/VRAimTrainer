using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows class be savable to a file. It can also be used to create a text file.
[System.Serializable]
public class PlayerScore
{
    public string userName;
    public string gameMode;
    public int score;
    public int time;
    public float hits;
    public int misses;
    public float accuracy;

    public override string ToString()
    {
        return "Username: " + userName + "GameMode: " + gameMode + "Score: " + score + "Time: " + time;
    }
}
