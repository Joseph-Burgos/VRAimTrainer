using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS IS A PLAYERSCORE CLASS. IT CAN BE USED TO CREATE A TEXT FILE

// Used to record key aspects of a played game.
// A player is assigned a score.
// This class is used to structure data that will be written to and read from disk when calculating VisualFeedback.
[System.Serializable]
public class PlayerScore
{
    public System.DateTime dateTime;
    public string gameMode;
    public int score;
    public int gameTime;
    // TODO add fields for accuracy, etc.
    public float accuracy;

    public override string ToString()
    {
        return "Date & time: " + dateTime + "GameMode: " + gameMode + "Score: " + score + "Time: " + gameTime;
    }
}
