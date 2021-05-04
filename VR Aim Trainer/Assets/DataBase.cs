using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class DataBase
{

    public static void Savedata(Score newscore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        
        Playerscore score = new Playerscore(newscore);

        formatter.Serialize(stream, score);
        stream.Close();

    }

    public static Playerscore LoadScore()
    {
        string path = Application.persistentDataPath + "/player.txt";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Playerscore score = formatter.Deserialize(stream) as Playerscore;
            stream.Close();
            return score;
        }
        else
        {
            Debug.LogError("File not found in " + path);
            return null;
        }
    }

}