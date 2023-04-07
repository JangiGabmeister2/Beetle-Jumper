using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnter : MonoBehaviour
{
    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enterEvent.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            exitEvent.Invoke();
        }
    }
}
