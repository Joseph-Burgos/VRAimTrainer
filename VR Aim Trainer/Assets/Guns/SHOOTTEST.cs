using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SHOOTTEST : MonoBehaviour
{
    public GameObject laser;
    public SteamVR_Action_Boolean fireAction;
    public float range = 100f;
    public Transform muzzle;
    public bool useLaser = true;
    public bool constantFire = false;
    public ParticleSystem muzzleFlash = null;

    private bool isActive = false;
    private Interactable interactable;
    // Start is called before the first frame update
    void Start()
    {
        laser.SetActive(false);
        interactable = GetComponent<Interactable>();
    }
    // Update is called once per frame
    void Update()
    {
        //check if gun is being held
        if (interactable.attachedToHand != null)
        {
            //if laser is off, turn on
            if (!isActive)
            {
                Debug.Log("Turn On");
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
            Debug.Log("=====================LET GO====================================");
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
        //play audio
        //FindObjectOfType<AudioManager>().Play("GlockShot");
        //play muzzle particle
        muzzleFlash.Play();
        //store raycast information
        RaycastHit hit;
        if(Physics.Raycast(muzzle.transform.position, muzzle.transform.forward, out hit, range))
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
    }

    IEnumerator ShootEveryFrame()
    {
        shoot();
        yield return null;
    }
}
