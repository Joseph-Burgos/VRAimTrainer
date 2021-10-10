using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS A PLAYERSCORE CLASS. IT CAN BE USED TO CREATE A TEXT FILE

//lets class be savable to a file
[System.Serializable]
public class PlayerScore
{
    public string userName;
    public string gameMode;
    public int score;
    public int time;

    public override string ToString()
    {
        return "GameMode: " + gameMode + "Score: " + score + "Time: " + time;
    }
}
