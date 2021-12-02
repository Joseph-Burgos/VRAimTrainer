using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private static GameObject _player;

    void Awake()
    {
        _player = GameObject.Find("Player");
        _player.transform.position = gameObject.transform.position;
    }
}
