using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    public UnityEvent onPressed, onReleased;
    // Start is called before the first frame update
    void Start(){
        // grabs the starting position of the button
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update(){
        // checks if button is pressed
        if (!_isPressed && GetValue() + threshold >= 1){
            Pressed();
        }
        // checks if button is released
        if (_isPressed && GetValue() - threshold <= 0){
            Released();
        }
    }

    // returns a percentage of the difference in movement of the button
    private float GetValue(){
        var value = Math.Abs(Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit);

        if (Math.Abs(value) < deadZone){
            value = 0;
        }

        return Mathf.Clamp(value, -1f, 1f);
    }

    // updates _isPressed
    private void Pressed(){
        _isPressed = true;
        onPressed.Invoke();
        // Debug.Log("Pressed");
    }

    private void Released(){
        _isPressed = false;
        onReleased.Invoke();
        // Debug.Log("Released");
    }
}
