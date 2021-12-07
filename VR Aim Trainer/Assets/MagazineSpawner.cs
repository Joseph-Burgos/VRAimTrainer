using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineSpawner : MonoBehaviour
{
    public GameObject magazineToSpawn;
    public Transform positionToSpawn;

    public void spawnMagazine()
    {
        GameObject spawnedMag = Instantiate(magazineToSpawn);//create magazine
        spawnedMag.transform.position = positionToSpawn.position;//move magazine
    }
}
