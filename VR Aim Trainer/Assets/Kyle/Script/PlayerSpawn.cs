using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private static GameObject _player;

    void Awake()
    {
        try 
        {
            _player = GameObject.Find("Player");
            _player.transform.position = gameObject.transform.position;
        }
        catch (Exception e)
        {
            Debug.Log("Player Object Not Found");
        }
    }
}
