using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;
    private CharacterController characterController;

    private void Start()
    {
        // needed to move in the environment
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // calculate the move direction of the player
        Vector3 moveDirection = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
        // calculate gravity
        Vector3 gravity = new Vector3(0, 9.81f, 0) * Time.deltaTime;
        // apply direction and gravity to the character controller
        characterController.Move(speed * Time.deltaTime * Vector3.ProjectOnPlane(moveDirection, Vector3.up) - gravity);
        // FIXME: Hands collide with the playerController
    }
}
