using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SHOOTTEST : MonoBehaviour
{

    private bool isActive = false;
    private Interactable interactable;

    [Header("Info for class to function")]
    public GameObject laser;
    public SteamVR_Action_Boolean fireAction;
    public Transform muzzle;
    public BulletTrail bulletTrail;
    public ParticleSystem muzzleFlash = null;

    [Header("options for Gun")]
    public float RayCastRange = 100f;
    public bool useBulletTrail;
    public bool useLaser = true;
    public bool constantFire = false;

    [Header("game environment objects")]
    [SerializeField] GameObject GameSystem;
    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GameSystem.GetComponent<ScoreManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
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
            //else if pressing down fire button
            if (constantFire)
            {
                StartCoroutine("ShootEveryFrame");
            }
            else if (fireAction[source].stateDown)
            {
                shoot();
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
        //run if constant fire is off
        if (!constantFire)
        {  
            //play audio
            FindObjectOfType<AudioManager>().Play("GlockShot");

            //play muzzle particle
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

    IEnumerator ShootEveryFrame()
    {
        shoot();
        yield return null;
    }
}
