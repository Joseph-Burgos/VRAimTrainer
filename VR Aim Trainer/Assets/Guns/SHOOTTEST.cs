using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SHOOTTEST : MonoBehaviour
{

    private bool isActive = false;
    [SerializeField] private int ammo = 0;
    private bool magInserted = false;
    private Interactable interactable;
    private GameObject insertedMag;
    Transform SkinsTransform; 

    [Header("Required References")]
    public GameObject laser;
    public SteamVR_Action_Boolean fireAction;
    public SteamVR_Action_Boolean ejectMagAction;
    public Transform muzzle;
    public BulletTrail bulletTrail;
    public ParticleSystem muzzleFlash = null;
    public Transform magazineSlot;
    [Tooltip("The Magazine GameObject that will spawn when the ejectMag button is pressed")]
    public GameObject magazineToSpawn;

    [Header("Gun Options")]
    public float RayCastRange = 100f;
    public bool useBulletTrail;
    public bool useLaser = true;
    public bool constantFire = false;

    [Header("Game Environment Objects")]
    [SerializeField] GameObject GameSystem;
    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GameSystem.GetComponent<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the parent of skins
        SkinsTransform = GameObject.Find("Skins").transform;

        laser.SetActive(false);
        interactable = GetComponent<Interactable>();
    }
    // Update is called once per frame
    void Update()
    {
        //for testing---------
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }
        if (constantFire)
        {
            StartCoroutine("ShootEveryFrame");
        }
        //for testing---------

        //check if gun is being held
        if (interactable.attachedToHand != null)
        {
            //if laser is off, turn on
            if (!isActive)
            {
                //Debug.Log("Turn On");
                toggleLaser(true);
            }
            //access which hand is holding
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            //check if constant fire is true
            if (constantFire)
            {
                StartCoroutine("ShootEveryFrame");
            }
            //else if pressing down fire button and there is ammo in the weapon
            else if (fireAction[source].stateDown && ammo > 0)
            {
                ammo--;
                shoot();
                playAnim();
                playVfx();
            }

            //check if ejectMag button is pressed
            if(ejectMagAction[source].stateDown && magInserted)
            {
                Debug.Log("ejectMagazine activated");
                ejectMagazine(insertedMag);
            }
        }
        //checked if gun is being held AND laser is visible
        else if(interactable.attachedToHand == null && isActive)
        {
            //Debug.Log("=====================LET GO====================================");
            toggleLaser(false);
        }
    }

    private void toggleLaser(bool toggle)
    {
        if (useLaser)
        {
            isActive = toggle;
            laser.SetActive(toggle);
        }
    }

    private void shoot()
    {
        scoreManager.AddToShots();

        //store raycast information
        RaycastHit hit;
        if(Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, RayCastRange))
        {
            //Debug.Log(hit.transform.name);
            //store target component thats been hit
            Target_Parent currentTarget = hit.transform.GetComponent<Target_Parent>();
            //check if we hit an object that actually had a target component
            if(currentTarget != null)
            {
                currentTarget.hit();  
            }
        }
        //show debug of where its shooting
        Debug.DrawLine(muzzle.transform.position, muzzle.transform.position + muzzle.transform.forward * 100, Color.green ,3f);
        //fire bullet trail
        if (useBulletTrail && !constantFire)
        {
            bulletTrail.shootTrail();
        }
        
    }


    public void playVfx()
    {
        //play audio
        FindObjectOfType<AudioManager>().Play("GlockShot");

        ///play muzzle particle
        if (GameManager.Instance.useVFX)
        {
            //make sure not already running
            if (muzzleFlash.isPlaying)
            {
                muzzleFlash.Stop();
            }
            muzzleFlash.Play();
        }
    }
    public void playAnim()
    {
        //hard coded to hell, needs to check SkinManager to check which index is active
        for (int i = 0; i < 3; i++)
        {
            if (SkinsTransform.GetChild(i).gameObject.activeSelf)
            {
                SkinsTransform.GetChild(i).gameObject.GetComponent<Animator>().Play("Fire");
            }
        }
        //play animation at index of currently used skin
        
    }

    IEnumerator ShootEveryFrame()
    {
        shoot();
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if collision with gun is a magazine, if there is no magazine present, and if the magazine is not newly spawned.
        if (other.tag == "Magazine" && !magInserted && !other.GetComponent<magazineScript>().newlySpawned)
        {
            magInserted = true;

            //store the inserted magazine game object
            insertedMag = other.gameObject;

            //detach the magazine from hand
            other.GetComponent<Interactable>().attachedToHand.DetachObject(other.gameObject, false);

            //disable interactable script
            other.GetComponent<Interactable>().enabled = false;
            Destroy(other.GetComponent("Throwable"));

            //disable colliders on magazine so that it doesn't collide with weapon
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.GetComponent<BoxCollider>().isTrigger = true;

            //change rotation of mag to magazine slot
            other.transform.rotation = magazineSlot.rotation;
            //change position of mag to magazine slot
            other.transform.position = magazineSlot.position;
            //make the magazine a child of the gun and change the transform
            other.transform.SetParent(gameObject.transform);

            //move magazine object to the magazine slot in the gun
            other.gameObject.transform.position = magazineSlot.position;
            //set the ammo in the magazine to the gun
            if(ammo > 0)//means there is a bullet in the chamber
            {
                ammo = other.gameObject.GetComponent<magazineScript>().ammoCount;
                ammo++;
            }
            else
            {
                ammo = other.gameObject.GetComponent<magazineScript>().ammoCount;
            }

            Debug.Log("Detected magazine with " + other.gameObject.GetComponent<magazineScript>().ammoCount.ToString() + " ammo.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //checks if the magazine is a newly spawned magazine
        if(other.tag == "Magazine" && other.GetComponent<magazineScript>().newlySpawned)
        {
            //when the magazine leaves the collider area, the mag is no longer newly spawned
            other.GetComponent<magazineScript>().newlySpawned = false;
        }
    }

    private void ejectMagazine(GameObject magazine)
    {
        magInserted = false;

        //destroy the magazine object
        Destroy(magazine);
        //spawn a new magazine
        GameObject spawnedMag = Instantiate(magazineToSpawn);
        //set magazine to newly spawned, so that it does not go back into the gun when it collides with gun.
        spawnedMag.GetComponent<magazineScript>().newlySpawned = true;
        //move the mag to the magazineslot
        spawnedMag.transform.position = magazineSlot.transform.position;

        //*simulating a bullet in the chamber*
        //if there is still ammo in the mag before ejecting, let there be one more round left in the weapon
        //update the ammo on the magazine
        if (ammo != 0)
        {
            spawnedMag.GetComponent<magazineScript>().ammoCount = ammo - 1;
            ammo = 1;
        }
        else
        {
            spawnedMag.GetComponent<magazineScript>().ammoCount = 0;
            ammo = 0;
        }
    }
}
