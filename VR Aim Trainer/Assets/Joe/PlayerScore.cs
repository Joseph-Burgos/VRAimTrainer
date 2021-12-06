using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is used to structure data that will be written to and read from disk when calculating VisualFeedback.
// It is used to record key aspects of a played game.
[System.Serializable]
public class PlayerScore
{
    public string dateTime;
    public string gameMode;
    public int score;
    public int gameTime;
    public float accuracy;

    public override string ToString()
    {
        return "Date & time: " + dateTime + " GameMode: " + gameMode + " Score: " + score + " Time: " + gameTime;
    }
}
