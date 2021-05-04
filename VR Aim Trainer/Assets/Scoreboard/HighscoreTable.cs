using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transforam entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        //get access to the highscoreEntryContainer
        //get access to the highscoreEntryTempalte


        //get json file string
        //convert to high score object list

        //sort list

        //go through list of highscores and create a high score object to display
    }

}





/*
 * Will create a table of entries
 * highscoreEntry is the score to add
 * transform to the container
 * list we wioll add the instantiated objects
 */
private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList);
{
    //add a score from given parameters

}

private void AddHighscoreEntry(int score, string name)
{
    //create a new highscoreEntry object

    //add to save list
    //load list

    //add into highscoreEntryList

    //save updated list -- will be stored
}

/*
 * creates an object of the list to be able to save it into a json
 */
private class Highscores
{
    public List<HighscoreEntry> highscoreEntryList;
}

/*
 * represents a signle high score entry
 */
private class HighscoreEntry
{
    public int score;
    public string name;
}
