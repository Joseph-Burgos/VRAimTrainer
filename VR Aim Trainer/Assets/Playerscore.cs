using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Playerscore
{
    string name;
    int score;
    int time;



    public Playerscore(Score score)
    {
        name = score.name;
        this.score = score.score;
        time = score.time;
    }

}
