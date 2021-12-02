using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TPMainHub : MonoBehaviour
{
    public UnityEvent tp_Event;
    void Update()
    {
        tp_Event.Invoke();
    }
}
